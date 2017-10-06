using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Entities
{
    public class SudokuInfoContext : DbContext
    {
        public SudokuInfoContext(DbContextOptions<SudokuInfoContext> options)
           : base(options)
        {
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<DailySudoku> DailySudoku { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
