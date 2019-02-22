using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reversi.Controllers;

namespace Reversi.Pages
{
    public class IndexModel : PageModel
    {

        public void OnGet()
        {
            string username = HttpContext.Session.GetString("login");
            ViewData["Username"] = username;

            //GameController game = new GameController();
        }
        public void OnPost(string game)
        {
            if(game == "new")
            {
                GameController gameController = new GameController();
                //TODO game data to screen
            }
            else
            {

            }
        }
    }

}
