using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using CoordinateSharp;

[ApiController]
[Route("api/[controller]")]
public class AcreageController : ControllerBase
{
    private readonly GeometryFactory _geometryFactory;

    public AcreageController()
    {
        _geometryFactory = new GeometryFactory();
    }

    [HttpGet("calculate")]
    public ActionResult Calculate([FromQuery] double[] lat, [FromQuery] double[] lon)
    {
        if (lat == null || lon == null || lat.Length != lon.Length || lat.Length < 3)
        {
            return BadRequest("Please provide at least three pairs of latitude and longitude.");
        }

        var coords = lat
            .Select((latitude, index) => new CoordinateSharp.Coordinate(latitude, lon[index]))
            .Select(coord => new NetTopologySuite.Geometries.Coordinate(coord.UTM.Easting, coord.UTM.Northing)).ToArray();
        var polygon = _geometryFactory.CreatePolygon(coords);

        // Calculate area in acres
        double areaInAcres = polygon.Area / 4046.86;

        return Ok(new { acres = areaInAcres});
    }
}
