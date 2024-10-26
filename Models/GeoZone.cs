using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace geonet.Models;

[Table("GeoZone", Schema = "Application")]
public class GeoZone
{
    public int GeoZoneID { get; set; }

    // Database includes both Polygon and MultiPolygon values
    public Geometry Border { get; set; }

    [NotMapped] // Exclude UserData from EF Core's mapping
    public object UserData
    {
        get => Border?.UserData;
        set
        {
            if (Border != null)
            {
                Border.UserData = value;
            }
        }
    }

}
