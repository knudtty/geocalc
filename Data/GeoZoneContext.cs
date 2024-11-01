using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Geocalc.Models;

namespace Geocalc.Data
{
    public class GeoZoneContext : DbContext
    {
        public GeoZoneContext() { } // Parameterless constructor for scaffolding

        public GeoZoneContext(DbContextOptions<GeoZoneContext> options) : base(options) { }

        public DbSet<GeoZone> GeoZones { get; set; }
    }
}
