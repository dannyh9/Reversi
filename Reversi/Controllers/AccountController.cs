using Reversi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Controllers
{
    public class AccountController
    {
        public List<AccountModel> list { get; set; }
        public SqlConnection con = new SqlConnection(DatabaseModel.connectionString);

        public List<AccountModel> GetAccounts()
        {
            SqlCommand cmd = new SqlCommand("Select * from Users", con);
            con.Open();
            list = new List<AccountModel> { };
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    AccountModel accountmodel = new AccountModel();
                    accountmodel.Username = reader["Username"].ToString();
                    accountmodel.Id = int.Parse(reader["UserID"].ToString());
                    accountmodel.Email = reader["Email"].ToString();
                    accountmodel.Role = int.Parse(reader["Role"].ToString());

                    list.Add(accountmodel);
                }
            }
            cmd.Dispose();
            con.Close();
            return list;
        }

        public AccountModel GetAccount(int id)
        {
            AccountModel accountmodel = new AccountModel();

            SqlCommand cmd = new SqlCommand("Select * from Users where UserID = @id", con);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                accountmodel.Username = reader["Username"].ToString();
                accountmodel.Id = int.Parse(reader["UserID"].ToString());
                accountmodel.Email = reader["Email"].ToString();
                accountmodel.Role = int.Parse(reader["Role"].ToString());
            }

            reader.Close();
            reader.Dispose();
            cmd.Dispose();
            con.Close();

            return accountmodel;
        }
        public void UpdateAccount(string id, string username, string email, string password, string role)
        {
            if (password == null)
            {
                SqlCommand cmd = new SqlCommand("UPDATE Users SET Email=@email,Username=@username,Role=@role WHERE UserId=@id", con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@role", Convert.ToInt32(role));
                con.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            else
            {
                SqlCommand cmd = new SqlCommand("UPDATE Users SET Email=@email,Password=@password,Username=@username,Role=@role WHERE UserId=@id", con);
                cmd.Parameters.AddWithValue("@password", LoginController.SHA512(password));
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@role", Convert.ToInt32(role));
                con.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }

        }
        public void CreateAccount(string username, string email, string password, string role)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password, Email,Role) VALUES(@username,@password,@email,@role)", con);
            cmd.Parameters.AddWithValue("@password", LoginController.SHA512(password));
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@role", Convert.ToInt32(role));
            con.Open();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void DeleteAccount(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE UserId=@id", con);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
            con.Open();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}