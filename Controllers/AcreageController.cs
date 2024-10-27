using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using GeoAPI.CoordinateSystems.Transformations;


[ApiController]
[Route("api/[controller]")]
public class AcreageController : ControllerBase
{
    private readonly GeometryFactory _geometryFactory;
    private readonly CoordinateTransformationFactory _transformationFactory;
    private readonly ICoordinateTransformation _transformation;

    public AcreageController()
    {
        _geometryFactory = new GeometryFactory().WithSRID(4326);

        // Define source (WGS84) and target (UTM Zone 33N) coordinate systems
        var wgs84 = GeographicCoordinateSystem.WGS84;
        var utm33N = ProjectedCoordinateSystem.WGS84_UTM(33, true); // Modify for appropriate UTM zone

        _transformationFactory = new CoordinateTransformationFactory();
        _transformation = _transformationFactory.CreateFromCoordinateSystems(wgs84, utm33N);
    }

    [HttpGet("calculate")]
    public ActionResult Create([FromQuery] double[] lat, [FromQuery] double[] lon)
    {
        if (lat == null || lon == null || lat.Length != lon.Length || lat.Length < 3)
        {
            return BadRequest("Please provide at least three pairs of latitude and longitude.");
        }

        // Convert lat/lon to Coordinates in WGS84
        var wgs84Points = lat.Select((latitude, index) => new Coordinate(lon[index], latitude)).ToArray();

        // Transform coordinates from WGS84 to UTM
        var utmPoints = wgs84Points.Select(coord =>
        {
            var transformed = _transformation.MathTransform.Transform(new[] { coord.X, coord.Y });
            return new Coordinate(transformed[0], transformed[1]);
        }).ToArray();

        // Create a polygon from the transformed UTM coordinates
        var polygon = _geometryFactory.CreatePolygon(utmPoints);
        Console.WriteLine(polygon);

        // Calculate area in acres
        Console.WriteLine(polygon.Area);
        double areaInAcres = polygon.Area / 4046.86;

        return Ok(new { acres = areaInAcres });
    }
}
