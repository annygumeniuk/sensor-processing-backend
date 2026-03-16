using SensorProcessing.Devices.Abstraction;

namespace SensorProcessing.BusinessLogic.Services.Implementations
{
    public class SensorService
    {
        private readonly IDataTransport _transport;

        public SensorService(IDataTransport transport)
        {
            _transport = transport;
            _transport.OnMessageReceived += HandleMessage;
        }

        private void HandleMessage(string message)
        {
            Console.WriteLine($"BusinessLogic received: {message}");
            // TODO: Process the message, e.g. parse it, save to database, etc.
        }

        public async Task StartAsync()
        {
            await _transport.StartAsync();
        }

        public Task SendMessageAsync(string message)
        {
            return _transport.SendMessageAsync(message);
        }
    }
}
