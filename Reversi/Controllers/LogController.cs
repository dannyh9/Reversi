using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reversi.DAL;
using Reversi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reversi.Controllers
{
    public class LogController : Controller
    {
        private readonly LogContext _context;

        public LogController()
        {
        }

        public LogController(LogContext context) => _context = context;

        public List<LogModel> getLog ()
        {
            return _context.Log.ToList();
        }

    }
}
