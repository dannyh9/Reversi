using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reversi.Controllers;
using Reversi.Models;

namespace Reversi.Pages
{
    public class AccountsModel : PageModel
    {
        public List<AccountModel> list { get; set; }

        public void OnGet()
        {
            if (HttpContext.Session.GetString("role") == null)
            {
                Response.Redirect("Login");
            }
            ViewData["role"] = HttpContext.Session.GetString("role").ToString();
            AccountController Accountcontroller = new AccountController();
            list = Accountcontroller.GetAccounts();
        }
    }
}