using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reversi.Controllers;

namespace Reversi.Pages
{
    public class LoginModel : PageModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string loginmsg = "";
        public void OnGet()
        {
            if (HttpContext.Session.GetString("login") != null)
            {
                ViewData["login"] = "true";
            }
        }

        public void OnPost(string Username, string Password)
        {
            LoginController Logincontroller = new LoginController();
            bool login = Logincontroller.HandleLogin(Username, Password);
            if (login == true)
            {
                HttpContext.Session.SetString("login", Logincontroller.loginModel.Username);
                Response.Redirect("Index");
            }
            else
            {
                loginmsg = "invalid username or password";
                ViewData["Error"] = loginmsg;
            }

        }

        public void OnPostLogout()
        {
            HttpContext.Session.Clear();
            Response.Redirect("Login");
        }
    }
}