using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Models
{
    public class LogModel
    {
        [Key]
        public int LogId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }

    }
}
