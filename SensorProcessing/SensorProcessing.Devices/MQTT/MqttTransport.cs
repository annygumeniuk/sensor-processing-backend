using SensorProcessing.Devices.Abstraction;
using System;
using System.Threading.Tasks;

namespace SensorProcessing.Devices.MQTT
{
    public class MqttTransport : IDataTransport
    {
        public event Action<string> OnMessageReceived;

        public Task SendMessageAsync(string message)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync()
        {
            throw new NotImplementedException();
        }
    }
}
