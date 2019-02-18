﻿using Reversi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Controllers
{
    public class LoginController
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public bool returnbool = false;
        public string ReturnMsg { get; set; }

        public Boolean HandleLogin(string username, string password)
        {
            SqlConnection con;
            con = new SqlConnection(DatabaseModel.connectionString);

            //select user
            SqlCommand cmd = new SqlCommand("Select * from Users where Username=@username", con);
            cmd.Parameters.AddWithValue("@username", username);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Boolean login = (password.Equals(reader["Password"].ToString(), StringComparison.InvariantCulture)) ? true : false;
                    if (login)
                    {

                        Username = reader["Username"].ToString();
                        Role = reader["Role"].ToString();
                        returnbool = true;
                    }
                    else
                        returnbool = false;
                }
                else
                    returnbool = false;

                reader.Close();
                reader.Dispose();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                //message = ex.Message.ToString() + "Error.";

            }
            return returnbool;
        }

        public void HandleRegister(string username, string password, string password2, string email)
        {
            if(username == null || password == null || password == null || email == null)
            {
                ReturnMsg = "Een van de velden is niet ingevuld";
            }else if (password != password2)
            {
                ReturnMsg = "Wachtwoorden komen niet overeen";
            }else
            {
                SqlConnection con;
                con = new SqlConnection(DatabaseModel.connectionString);
                //SqlCommand cmd = new SqlCommand("Select * from Users where Username=@username", con);
                SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password, Email) VALUES(@username,@password,@email)", con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);
                con.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                ReturnMsg = "Account aangemaakt";
            }
            //return returnbool;

        }
    }
}
