using System;
using System.Threading.Tasks;

namespace SensorProcessing.Devices.Abstraction
{
    public interface IDataTransport
    {
        Task StartAsync();
        event Action<string> OnMessageReceived;
        Task SendMessageAsync(string message);
    }
}
