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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChallengeUser>()
                .HasKey(t => new { t.ChallengeId, t.UserId });

            modelBuilder.Entity<Challenge>()
                .HasMany(i => i.AssigneesList)
                .WithOne(i => i.Challenge)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(i => i.AssignedChallengesList)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserUser>()
                .HasKey(t => new { t.UserId, t.User1Id });

            modelBuilder.Entity<User>()
                .HasMany(i => i.RequestedFriendshipsList)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(i => i.AcceptedFriendshipsList)
                .WithOne(i => i.User1)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DailySudokuUser>()
                .HasKey(t => new { t.DailySudokuId, t.UserId });

            modelBuilder.Entity<DailySudoku>()
                .HasMany(i => i.ScoresList)
                .WithOne(i => i.DailySudoku)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(i => i.DailySudokuScoresList)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<DailySudoku> DailySudoku { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
