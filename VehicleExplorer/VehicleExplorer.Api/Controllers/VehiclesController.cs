using Microsoft.AspNetCore.Mvc;

namespace VehicleExplorer.Api.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehiclesController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet("makes")]
    public async Task<IActionResult> GetMakes(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50)
    {
        var makes = (await _vehicleService.GetMakes()).ToList();

        var total = makes.Count;

        var data = makes
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Ok(new
        {
            page,
            pageSize,
            total,
            data
        });
    }

    [HttpGet("{makeId}/types")]
    public async Task<IActionResult> GetTypes(int makeId)
    {
        var types = await _vehicleService.GetVehicleTypes(makeId);

        return Ok(types);
    }

    [HttpGet("{makeId}/{year}/models")]
    public async Task<IActionResult> GetModels(int makeId, int year)
    {
        var models = await _vehicleService.GetModels(makeId, year);

        return Ok(models);
    }

    [HttpGet("makes/search")]
    public async Task<IActionResult> SearchMakes(string term)
    {
        var makes = await _vehicleService.GetMakes();

        var filtered = makes
            .Where(x => x.Name.Contains(term,
            StringComparison.OrdinalIgnoreCase))
            .Take(20);

        return Ok(filtered);
    }
}