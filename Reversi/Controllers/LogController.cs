using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reversi.DAL;
using Reversi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reversi.Controllers
{
    public class LogController
    {
        public SqlConnection con = new SqlConnection(DatabaseModel.connectionString);

        public void addToLog(string name, string details)
        {
            string DateTime = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Log (Name, Details,Time) VALUES(@name,@details,@time)", con);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@details", details);
            cmd.Parameters.AddWithValue("@time", DateTime);

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}
