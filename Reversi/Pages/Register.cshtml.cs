using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reversi.Controllers;

namespace Reversi.Pages
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost(string Username, string Password , string Password2 , string Email)
        {
            LoginController Logincontroller = new LoginController();
            Logincontroller.HandleRegister(Username, Password, Password2, Email);
            
            if(Logincontroller.loginModel.ReturnMsg != null)
            ViewData["bericht"] = Logincontroller.loginModel.ReturnMsg;


            //LoginController Logincontroller = new LoginController();

            //if (login == true)
            //{
            //    HttpContext.Session.SetString("login", Logincontroller.Username);
            //    Response.Redirect("Index");
            //}
            //else
            //{
            //    loginmsg = "invalid username or password";
            //    ViewData["Error"] = loginmsg;
            //}

        }
    }
}