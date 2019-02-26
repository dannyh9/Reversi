using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reversi.Pages
{
    public class VerifyLoginModel : PageModel
    {
        public void OnGet()
        {
            ViewData["error"] = "Zie email voor code";
        }
        public void OnPost(string code)
        {
            if(code == HttpContext.Session.GetString("verify"))
            {
                HttpContext.Session.SetString("login", HttpContext.Session.GetString("loginnotverifyd"));
                HttpContext.Session.SetString("role", HttpContext.Session.GetString("rolenotverifyd"));
                Response.Redirect("Index");
            }
            else
            {
                //geef error code onjuist
                ViewData["error"] = "code niet juist";
            }
        }
    }
}