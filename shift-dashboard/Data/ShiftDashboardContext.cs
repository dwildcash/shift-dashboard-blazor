using Microsoft.EntityFrameworkCore;
using shift_dashboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shift_dashboard.Data
{
    public class ShiftDashboardContext: DbContext
    {
        public DbSet<DelegateDB> DelegatesDB { get; set; }

        public DbSet<VoterDB> VotersDB { get; set; }

        public ShiftDashboardContext(DbContextOptions<ShiftDashboardContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
