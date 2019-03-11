using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reversi.Controllers;
using Reversi.DAL;
using Reversi.Models;

namespace Reversi.Pages
{
    public class IndexModel : PageModel
    {
        public string Error { get; set; }

        private readonly LogContext _context;

        public IndexModel(LogContext context) => _context = context;

        public List<GameModel> gamesList { get; set; }

        public void OnGet()
        {
            Error = "";
            string username = HttpContext.Session.GetString("login");
            ViewData["Username"] = username;
            gamesList = _context.Games.ToList();
        }

        //TODO GET current game. 

        //GameController game = new GameController();
    }
}