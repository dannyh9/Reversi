using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Please enter your Username")]
        [Display(Name = "Username : ")]
        public string UserId { get; set; }

        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your Password.")]
        [Display(Name = "Password : ")]
        public string Password { get; set; }


        //This method validates the Login credentials
        public string LoginProcess(string strUsername, string strPassword)
        {
            string message = "";
            //my connection string
            SqlConnection con = new SqlConnection(Database.connectionstring);
            SqlCommand cmd = new SqlCommand("Select * from Users where UserId=@Username", con);
            cmd.Parameters.AddWithValue("@Username", strUsername);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Boolean login = (strPassword.Equals(reader["Password"].ToString(), StringComparison.InvariantCulture)) ? true : false;
                    if (login)
                    {
                        message = "1";
                        Username = reader["UserName"].ToString();

                    }
                    else
                        message = "Invalid Credentials";
                }
                else
                    message = "Invalid Credentials";

                reader.Close();
                reader.Dispose();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString() + "Error.";

            }
            return message;
        }

    }
}
    }
}
