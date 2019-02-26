using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reversi.Pages
{
    public class AccountModel : PageModel
    {
        public void OnGet()
        {
            if (HttpContext.Session.GetString("role") == null)
            {
                Response.Redirect("Login");
            }
        }
    }
}