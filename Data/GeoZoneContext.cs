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
    }
}
