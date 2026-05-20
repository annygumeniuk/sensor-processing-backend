using Microsoft.AspNetCore.Mvc;
using SensorProcessing.WebApi.Services;

namespace SensorProcessing.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataSourceController : ControllerBase
    {
        private readonly DataSourceManager _manager;

        public DataSourceController(DataSourceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult Get() =>
            Ok(new { mode = _manager.CurrentMode.ToString() });

        [HttpPut]
        public IActionResult Set([FromBody] SetDataSourceRequest request)
        {
            if (!Enum.TryParse<DataSourceMode>(request.Mode, ignoreCase: true, out var mode))
                return BadRequest(new { error = "Invalid mode. Valid values: WebApi, Serial, MQTT" });

            if (mode != DataSourceMode.WebApi)
            {
                var health = _manager.CheckIoTHealth();
                var isAvailable = mode == DataSourceMode.Serial ? health.SerialAvailable : health.MqttAvailable;
                if (!isAvailable)
                    return Conflict(new { error = $"{mode} is not available" });
            }

            _manager.SetMode(mode);
            return Ok(new { mode = _manager.CurrentMode.ToString() });
        }

        [HttpGet("health")]
        public IActionResult GetHealth()
        {
            var h = _manager.CheckIoTHealth();
            return Ok(new
            {
                serialAvailable = h.SerialAvailable,
                mqttAvailable = h.MqttAvailable,
                serialPort = h.SerialPort,
                availablePorts = h.AvailablePorts,
                anyIoTAvailable = h.SerialAvailable || h.MqttAvailable
            });
        }
    }

    public record SetDataSourceRequest(string Mode);
}
