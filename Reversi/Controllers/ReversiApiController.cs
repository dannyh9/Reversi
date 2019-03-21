using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Reversi.Models;
using Microsoft.EntityFrameworkCore;
using Reversi.DAL;
using Newtonsoft.Json;

namespace Reversi.Controllers
{
    

    [Route("api/reversi/game")]
    [ApiController]
    public class ReversiGameController : ControllerBase
    {
        private readonly LogContext _context;
        public ReversiGameController(LogContext context) => _context = context;
        GameController game = new GameController();

        [Route("{id}")]
        [HttpGet]
        public string GetGame(int id)
        {
            var game = _context.Games.First(a => a.GameId == id);
            string result = game.Field.ToString();
            return result;
        }

        [Route("{id}/turn")]
        [HttpGet]
        public string GetTurn(int id)
        {
            var game = _context.Games.First(a => a.GameId == id);

            return game.Turn.ToString();
        }


        [HttpPost]
        public string PostNewGame([FromBody] JObject data)
        {
            string gameName = "";
            int spelerId = 0;

            string message = "";
            //TODO make new game in database
            foreach (var x in data)
            {
                string name = x.Key;
                JToken token = x.Value;
                if(name == "id")
                {
                    spelerId = token.ToObject<int>(); ;
                } else if (name == "name")
                {
                    gameName = token.ToString();
                }
            }
            if(spelerId != 0)
            {
                
                game.fillNewGame();
                string fieldJson = JsonConvert.SerializeObject(game.field, Formatting.Indented);

                GameModel gameData = new GameModel();
                gameData.Name = gameName;
                gameData.Player1 = spelerId;
                //wit begint
                gameData.Turn = 1;
                gameData.Field = fieldJson;
                

               
                //int[,] field = JsonConvert.DeserializeObject<int[,]>(json);
                _context.Games.Add(gameData);
                _context.SaveChanges();
                message = "spel aangemaakt";
                //context
                //game.field;
            }else
            {
                message = "fout";
            }

            return message;

        }


        [Route("{id}")]
        [HttpPost]
        public string PostUpdateGame(int id ,[FromBody] JObject data)
        {
            int x = 0;
            int y = 0;
            int color = 0;

            foreach (var i in data)
            {
                string name = i.Key;
                JToken token = i.Value;
                if (name == "X")
                {
                    x = token.ToObject<int>();
                }
                else if (name == "Y")
                {
                    y = token.ToObject<int>();
                }
                else if (name == "Color")
                {
                    color = token.ToObject<int>();
                }
            }

            var game = _context.Games.First(a => a.GameId == id);
            if(game.Turn == color)
            {
                GameController gameController = new GameController();
                int[,] fieldFromJson = JsonConvert.DeserializeObject<int[,]>(game.Field);
                gameController.field = fieldFromJson;

                gameController.doTurn(x, y,color);

                string fieldJson = JsonConvert.SerializeObject(gameController.field, Formatting.Indented);

                game.Field = fieldJson;
                if (game.Turn == 1)
                    game.Turn = 2;
                else
                    game.Turn = 1;

                _context.Update(game);
                _context.SaveChanges();

                return "zet gedaan";
                
            }else
            {
                return "deze kleur is niet aan zet";
            }

           
        }
    }
}