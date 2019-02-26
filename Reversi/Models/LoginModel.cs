using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public bool returnbool = false;
        public string ReturnMsg { get; set; }
        public string Auth { get; set;}
        public Guid token { get; set; }
    }
}
