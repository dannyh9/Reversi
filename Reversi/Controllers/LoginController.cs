using Microsoft.AspNetCore.Http;
using Reversi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Controllers
{
    public class LoginController
    {
        public LoginModel loginModel = new LoginModel();

        LogController Logcontroller = new LogController();
        

        public SqlConnection con = new SqlConnection(DatabaseModel.connectionString);

        public Boolean HandleLogin(string username, string password)
        {
            //select user
            SqlCommand cmd = new SqlCommand("Select * from Users where Username=@username", con);
            cmd.Parameters.AddWithValue("@username", username);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Boolean login = (SHA512(password).Equals(reader["Password"].ToString(), StringComparison.InvariantCulture)) ? true : false;
                    if (login)
                    {

                        loginModel.Username = reader["Username"].ToString();
                        loginModel.Role = int.Parse(reader["Role"].ToString());
                        loginModel.returnbool = true;
                    }
                    else
                    {
                        loginModel.returnbool = false;
                        Logcontroller.addToLog(username, "voert verkeerde wachtwoord in");
                    }
                        
                }
                else
                    loginModel.returnbool = false;

                reader.Close();
                reader.Dispose();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                //message = ex.Message.ToString() + "Error.";

            }
            return loginModel.returnbool;
        }

        public void HandleRegister(string username, string password, string password2, string email)
        {
            if (username == null || password == null || password2 == null || email == null)
            {
                loginModel.ReturnMsg = "Een van de velden is niet ingevuld";
            }
            else if (password != password2)
            {
                loginModel.ReturnMsg = "Wachtwoorden komen niet overeen";
            }
            else
            {
                SqlCommand cmd2 = new SqlCommand("Select * from Users where Username=@username OR Email=@email", con);
                cmd2.Parameters.AddWithValue("@username", username);
                cmd2.Parameters.AddWithValue("@email", email);

                con.Open();
                SqlDataReader reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    Boolean userbool = (username.Equals(reader["Username"].ToString(), StringComparison.InvariantCulture)) ? true : false;
                    Boolean emailbool = (email.Equals(reader["Email"].ToString(), StringComparison.InvariantCulture)) ? true : false;
                    if (emailbool)
                    {
                        //email is in database
                        loginModel.ReturnMsg = "Email word al gebruikt";
                    }else if (userbool)
                    {
                        loginModel.ReturnMsg = "Username word al gebruikt";
                    }
                    else
                    {
                        loginModel.ReturnMsg = "error";
                    }
                }
                else
                {

                    if (password.Length < 10)
                    {
                        loginModel.ReturnMsg = "wachtwoord moet minimaal 10 tekens bevatten";
                    }
                    else if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsNumber))
                    {
                        loginModel.ReturnMsg = "wachtwoord moet minimaal een hoofdletter, een kleineletter en een nummber bevatten";
                    }
                    else
                    {
                        //wachtwoord zijn goed
                        con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password, Email) VALUES(@username,@password,@email)", con);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", SHA512(password));
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        con.Close();
                        loginModel.ReturnMsg = "Wachtwoord is aangepast";
                        Logcontroller.addToLog(email, "Wachtwoord is aangepast");
                    }
                }
                reader.Close();
                reader.Dispose();
                cmd2.Dispose();
                con.Close();
            }

        }

        public void HandleLostPassword(string email)
        {
            loginModel.token = Guid.NewGuid();
            try
            {
                //check mail in database 
                SqlCommand cmd = new SqlCommand("Select * from Users where Email=@email", con);
                cmd.Parameters.AddWithValue("@email", email);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    //send mail if mail is in database
                    SendRecoveryMail(email);
                    Logcontroller.addToLog(email, "Vergeten wachtwoord opgevraagd en mail verzonden");
                }
                else
                {
                    loginModel.ReturnMsg = "Account met dit email bestaat niet, maak een account aan of voer een ander mail address in.";
                }

                reader.Close();
                reader.Dispose();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        public void SendRecoveryMail(string email)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("dannyreversi@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Wachtwoord reset Reversi";
            mail.Body = "Klik op onderstaande link om het wachtwoord te resetten deze link is 30 minuten geldig " +
                " https://localhost:44323/Lost?token=" + loginModel.token + " ";
            //TODO url + controlle

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("dannyreversi@gmail.com", "0645604582");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            loginModel.ReturnMsg = "Wachtwoord reset mail verzonden, de mail is 30 minuten geldig.";
        }

        public void ChangePassword(string password1, string password2 , string email)
        {
            if (password1 == null || password1 != password2)
            {
                loginModel.ReturnMsg = "wachtwoorden komen niet overeen of zijn niet ingevuld";
            }else
            {
                if (password1.Length < 10)
                {
                    loginModel.ReturnMsg = "wachtwoord moet minimaal 10 tekens bevatten";
                }
                else if (!password1.Any(char.IsUpper) || !password1.Any(char.IsLower) || !password1.Any(char.IsNumber))
                {
                    loginModel.ReturnMsg = "wachtwoord moet minimaal een hoofdletter, een kleineletter en een nummber bevatten";
                }
                else
                {
                    //wachtwoord zijn goed
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Users SET Password=@password WHERE Email=@email", con);
                    cmd.Parameters.AddWithValue("@password", SHA512(password1));
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    loginModel.ReturnMsg = "Wachtwoord is aangepast";
                    Logcontroller.addToLog(email, "Wachtwoord is aangepast");
                }
            }
        }

        public void ChangePasswordUsername(string password1, string password2, string username)
        {
            if (password1 == null || password1 != password2)
            {
                loginModel.ReturnMsg = "wachtwoorden komen niet overeen of zijn niet ingevuld";
            }
            else
            {
                if (password1.Length < 10)
                {
                    loginModel.ReturnMsg = "wachtwoord moet minimaal 10 tekens bevatten";
                }else if (!password1.Any(char.IsUpper) || !password1.Any(char.IsLower) || !password1.Any(char.IsNumber))
                {
                    loginModel.ReturnMsg = "wachtwoord moet minimaal een hoofdletter, een kleineletter en een nummber bevatten";
                }else
                {
                    //wachtwoord zijn goed
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Users SET Password=@password WHERE Username=@username", con);
                    cmd.Parameters.AddWithValue("@password", SHA512(password1));
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    loginModel.ReturnMsg = "Wachtwoord is aangepast";
                    Logcontroller.addToLog(username, "Wachtwoord is aangepast");
                }
                
           
            }
        }

        public static string SHA512(string text)
        {
            string salt = "geheim123";
            var result = default(string);

            using (var algo = new SHA512Managed())
            {
                result = GenerateHashString(algo, salt+text);
            }

            return result;
        }

        public static string GenerateHashString(HashAlgorithm algo, string text)
        {
            // Compute hash from text parameter
            algo.ComputeHash(Encoding.UTF8.GetBytes(text));

            // Get has value in array of bytes
            var result = algo.Hash;

            // Return as hexadecimal string
            return string.Join(
                string.Empty,
                result.Select(x => x.ToString("x2")));
        }
    }
}
