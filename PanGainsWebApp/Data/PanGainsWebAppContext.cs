using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PanGainsWebApp.Models;

namespace PanGainsWebApp.Data
{
    public class PanGainsWebAppContext : DbContext
    {
        public PanGainsWebAppContext(DbContextOptions<PanGainsWebAppContext> options) : base(options)
        {
        }

        public DbSet<Account>? Account { get; set; }
        public DbSet<ChallengeStats>? ChallengeStats { get; set; }
        public DbSet<CompletedWorkout>? CompletedWorkout { get; set; }
        public DbSet<DaysWorkedOut>? DaysWorkedOut { get; set; }
        public DbSet<Exercise>? Exercise { get; set; }
        public DbSet<Folder>? Folder { get; set; }
        public DbSet<Leaderboard>? Leaderboard { get; set; }
        public DbSet<Routine>? Routine { get; set; }
        public DbSet<Set>? Set { get; set; }
        public DbSet<Social>? Social { get; set; }
        public DbSet<Statistics>? Statistics { get; set; }
        public DbSet<YourExercise>? YourExercise { get; set; }

    }
}
