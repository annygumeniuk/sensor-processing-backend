using SensorProcessing.Devices.Abstraction;
using SensorProcessing.Devices.Common;
using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace SensorProcessing.Devices.Serial
{
    public class SerialTransport: IDataTransport
    {
        private SerialPort _port;
        public event Action<string> OnMessageReceived;

        public SerialTransport(string portName)
        {
            _port = new SerialPort(portName, Constants.BOUND_RATE);
            _port.DataReceived += (s, e) =>
            {
                var line = _port.ReadLine();
                OnMessageReceived?.Invoke(line);
            };
        }

        public Task StartAsync()
        {
            _port.Open();
            return Task.CompletedTask;
        }

        public Task SendMessageAsync(string message)
        {
            _port.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
