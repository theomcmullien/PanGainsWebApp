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
        public PanGainsWebAppContext (DbContextOptions<PanGainsWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<PanGainsWebApp.Models.Account>? Account { get; set; }
        public DbSet<PanGainsWebApp.Models.ChallengeStats>? ChallengeStats { get; set; }
        public DbSet<PanGainsWebApp.Models.CompletedWorkout>? CompletedWorkout { get; set; }
        public DbSet<PanGainsWebApp.Models.DaysWorkedOut>? DaysWorkedOut { get; set; }
        public DbSet<PanGainsWebApp.Models.Exercise>? Exercise { get; set; }
        public DbSet<PanGainsWebApp.Models.Folder>? Folder { get; set; }
        public DbSet<PanGainsWebApp.Models.Leaderboard>? Leaderboard { get; set; }
        public DbSet<PanGainsWebApp.Models.Routine>? Routine { get; set; }
        public DbSet<PanGainsWebApp.Models.Set>? Set { get; set; }
        public DbSet<PanGainsWebApp.Models.Social>? Social { get; set; }
        public DbSet<PanGainsWebApp.Models.Statistics>? Statistics { get; set; }
        public DbSet<PanGainsWebApp.Models.YourExercise>? YourExercise { get; set; }
    }
}
