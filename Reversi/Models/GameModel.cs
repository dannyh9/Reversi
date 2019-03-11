using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Models
{
    public class GameModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Field { get;set; }
        public int Player1 { get; set; }
        public int? Player2 { get; set; }
        public int Turn { get; set; }
    }
}
