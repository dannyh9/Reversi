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
            string username = HttpContext.Session.GetString("login");
            ViewData["Username"] = username;
        }
        public void OnPost(string password1, string password2)
        {
            LoginController Logincontroller = new LoginController();

            Logincontroller.ChangePasswordUsername(password1, password2, HttpContext.Session.GetString("login"));
            ViewData["msg"] = Logincontroller.loginModel.ReturnMsg;
        }
    }
}