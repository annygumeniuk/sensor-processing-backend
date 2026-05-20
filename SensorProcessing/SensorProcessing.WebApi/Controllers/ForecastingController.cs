using Microsoft.AspNetCore.Mvc;
using SensorProcessing.Forecasting.Services;

namespace SensorProcessing.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ForecastingController : ControllerBase
{
    private readonly IForecastingService _forecastingService;

    public ForecastingController(IForecastingService forecastingService)
    {
        _forecastingService = forecastingService;
    }

    /// <summary>
    /// Accepts a CSV file and returns Actual vs Forecast chart data plus accuracy metrics.
    /// </summary>
    /// <param name="file">CSV with columns: Timestamp, Temperature (°C), Humidity (%), Pressure (hPa), Wind Speed (m/s)</param>
    /// <param name="horizonHours">Forecast horizon in hours (1–72). Defaults to 24.</param>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Forecast(
        IFormFile file,
        [FromForm] int horizonHours = 24)
    {
        if (file is null || file.Length == 0)
            return BadRequest("A non-empty CSV file is required.");

        if (horizonHours is < 1 or > 72)
            return BadRequest("horizonHours must be between 1 and 72.");

        string csvContent;
        using (var reader = new StreamReader(file.OpenReadStream()))
            csvContent = await reader.ReadToEndAsync();

        try
        {
            var result = _forecastingService.Forecast(csvContent, horizonHours);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
