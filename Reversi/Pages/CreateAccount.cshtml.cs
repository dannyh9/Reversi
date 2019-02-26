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
    public class CreateAccountModel : PageModel
    {
        AccountController Accountcontroller = new AccountController();

        public void OnGet()
        {
            if (HttpContext.Session.GetString("role") != "3")
            {
                Response.Redirect("Login");
            }
        }
        public void OnPost(string username, string email, string password, string role)
        {
            Accountcontroller.CreateAccount(username, email, password, role);
            Response.Redirect("Account");
        }
    }
}