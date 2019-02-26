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
    public class DeleteAccountModel : PageModel
    {
        AccountController Accountcontroller = new AccountController();
        public void OnGet(int id)
        {
            if (HttpContext.Session.GetString("role") != "3")
            {
                Response.Redirect("Login");
            }
            Accountcontroller.DeleteAccount(id);
            Response.Redirect("Account");
        }
    }
}