using Microsoft.EntityFrameworkCore;
using Reversi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.DAL
{
    public class LogContext : DbContext
    {
        public LogContext(DbContextOptions<LogContext> options) : base(options){ }
        public DbSet<LogModel> Log { get; set; }
        public DbSet<GameModel> Games { get; set; }
    }
}
