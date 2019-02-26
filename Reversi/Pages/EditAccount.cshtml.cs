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
    public class EditAccountModel : PageModel
    {
        public AccountModel account { get; set; }
        AccountController Accountcontroller = new AccountController();
        
        public void OnGet(int id)
        {
            if (HttpContext.Session.GetString("role") != "3")
            {
                Response.Redirect("Login");
            }
            account = Accountcontroller.GetAccount(id);
            ViewData["id"] = id.ToString();
            ViewData["email"] = account.Email.ToString();
            ViewData["username"] = account.Username.ToString();
            ViewData["role"] = account.Role.ToString();
        }
        public void OnPost(string id, string username, string email, string password,string role)
        {
            Accountcontroller.UpdateAccount(id, username, email, password, role);
            Response.Redirect("Account");
        }
    }
}