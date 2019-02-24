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
                }
            }
        }

        public void OnPost(string Email)
        {

            LoginController Logincontroller = new LoginController();
            Logincontroller.HandleLostPassword(Email);

            HttpContext.Session.SetString("token", Logincontroller.token.ToString());

            ViewData["msg"] = Logincontroller.ReturnMsg;

            //    if (login == true)
            //    {
            //        HttpContext.Session.SetString("login", Logincontroller.Username);
            //        Response.Redirect("Index");
            //    }
            //    else
            //    {
            //        loginmsg = "invalid username or password";
            //        ViewData["Error"] = loginmsg;
            //    }

        }
    }
}