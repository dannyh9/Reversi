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
    public class ChangeModel : PageModel
    {
        public void OnGet()
        {
            if (HttpContext.Session.GetString("role") == null)
            {
                Response.Redirect("Login");
            }
        }
        public void OnPost(string password1, string password2)
        {
            LoginController Logincontroller = new LoginController();

            if (HttpContext.Session.GetString("login") == null)
            {
                Response.Redirect("Login");
            }

            Logincontroller.ChangePasswordUsername(password1, password2, HttpContext.Session.GetString("login"));
            ViewData["msg"] = Logincontroller.loginModel.ReturnMsg;
        }
    }
}