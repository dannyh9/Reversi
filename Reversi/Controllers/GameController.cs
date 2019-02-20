using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Controllers
{
    public class GameController
    {
        public FieldController field { get; set; }
        public Guid guid { get; set; }
        
        public GameController()
        {
            field = new FieldController();
            guid = new Guid();
        }
    }
}
