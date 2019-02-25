using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reversi.Controllers;
using Reversi.DAL;
using Reversi.Models;

namespace Reversi.Pages
{
    public class LogsModel : PageModel
    {
        private readonly LogContext _context;

        public LogsModel(LogContext context) => _context = context;

        public List<LogModel> list { get; set; }

        public void getLog()
        {
            list =  _context.Log.ToList();
        }

        public void OnGet()
        {
            getLog();
        }
    }
}