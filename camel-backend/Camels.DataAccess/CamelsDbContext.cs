using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camels.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Camels.DataAccess
{
    public class CamelsDbContext : DbContext
    {
        public DbSet<Camel> Camels { get; set; } = null!;

        public CamelsDbContext(DbContextOptions<CamelsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camel>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Camel>()
                .ToTable(t =>
                    t.HasCheckConstraint(
                        "CK_Camels_HumpCount",
                        "HumpCount IN (1, 2)"
                    ));
        }
    }
}
