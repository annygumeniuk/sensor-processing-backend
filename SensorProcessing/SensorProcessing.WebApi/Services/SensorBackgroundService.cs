using SensorProcessing.BusinessLogic.Services.Implementations;

namespace SensorProcessing.WebApi.Services
{
    public class SensorBackgroundService : BackgroundService
    {
        private readonly SensorService _sensorService;

        public SensorBackgroundService(SensorService sensorService)
        {
            _sensorService = sensorService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Running SensorBackgroundService...");

            // Runnin transport layer to read data from sensors
            await _sensorService.StartAsync();

            // Never-ending loop to keep the service running until cancellation is requested
            while (!stoppingToken.IsCancellationRequested)
            {
                // Add any periodic tasks here: logging, health checks, etc.                
                await Task.Delay(1000, stoppingToken);
            }

            Console.WriteLine("SensorBackgroundService stopped.");
        }
    }
}
