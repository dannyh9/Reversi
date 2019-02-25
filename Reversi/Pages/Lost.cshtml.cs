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
    public class LostModel : PageModel
    {
        public void OnGet()
        {
            if (HttpContext.Session.GetString("token") != null)
            {
                if(HttpContext.Request.Query["token"].ToString() == HttpContext.Session.GetString("token"))
                {
                    ViewData["validToken"] = "true";

                    ViewData["email"] = HttpContext.Session.GetString("email");
                }
            }
        }

        public void OnPost(string Email)
        {

            LoginController Logincontroller = new LoginController();
            Logincontroller.HandleLostPassword(Email);

            HttpContext.Session.SetString("token", Logincontroller.loginModel.token.ToString());
            HttpContext.Session.SetString("email", Email);

            ViewData["msg"] = Logincontroller.loginModel.ReturnMsg;

        }
        public void OnPostReset(string password1, string password2,string email)
        {
            LoginController Logincontroller = new LoginController();

            
            Logincontroller.ChangePassword(password1, password2, email);
            ViewData["msg"] = Logincontroller.loginModel.ReturnMsg;
        }
    }
}