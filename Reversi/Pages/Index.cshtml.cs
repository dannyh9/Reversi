using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reversi.Pages
{
    public class IndexModel : PageModel
    {
       
        public void OnGet()
        {
            string username = HttpContext.Session.GetString("login");
            ViewData["Username"] = username;
        }
    }
}
