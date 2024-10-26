using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using geonet.Models;

namespace geonet.Data
{
    public class GeoZoneContext : DbContext
    {
        public GeoZoneContext() { } // Parameterless constructor for scaffolding

        public GeoZoneContext(DbContextOptions<GeoZoneContext> options) : base(options) { }

        public DbSet<GeoZone> GeoZones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure spatial data and ignore unsupported properties
            modelBuilder.Entity<GeoZone>()
                .Property(g => g.Border)
                .HasColumnType("GEOMETRY"); // SQLite spatial type

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=geozones.db", x => x.UseNetTopologySuite());
            }
        }
    }
}
