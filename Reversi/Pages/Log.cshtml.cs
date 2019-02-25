using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reversi.Controllers;

namespace Reversi.Pages
{
    public class LogModel : PageModel
    {
        public void OnGet()
        {
            LogController logController = new LogController();
        }
    }
}