using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Reversi.Controllers.FieldController;

namespace Reversi.Controllers
{
    public class GameController
    {
        public FieldController field { get; set; }
        public Guid guid { get; set; }
        public Kleur kleur { get; set; }


        public GameController()
        {
            field = new FieldController();
            guid = new Guid();
        }

        public void doTurn(int x,int y, string turn)
        {
            if (turn == "wit")
            {
                kleur = Kleur.White;
            } else
            {
                kleur = Kleur.Black;
            }
            field.DoTurn(x,y, kleur);
        }
    }
}
