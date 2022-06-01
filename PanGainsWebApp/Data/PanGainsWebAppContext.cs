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
    }
}
