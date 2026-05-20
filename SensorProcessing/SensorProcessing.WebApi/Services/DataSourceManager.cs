using System.IO.Ports;

namespace SensorProcessing.WebApi.Services
{
    public enum DataSourceMode
    {
        WebApi,
        Serial,
        MQTT
    }

    public record IoTHealthResult(
        bool SerialAvailable,
        bool MqttAvailable,
        string SerialPort,
        string[] AvailablePorts
    );

    public class DataSourceManager
    {
        private DataSourceMode _currentMode = DataSourceMode.WebApi;
        private readonly string _serialPortName;

        public DataSourceManager(IConfiguration configuration)
        {
            _serialPortName = configuration["IoT:SerialPort"] ?? "COM5";
        }

        public DataSourceMode CurrentMode => _currentMode;

        public void SetMode(DataSourceMode mode) => _currentMode = mode;

        public IoTHealthResult CheckIoTHealth()
        {
            var availablePorts = SerialPort.GetPortNames();
            return new IoTHealthResult(
                SerialAvailable: availablePorts.Contains(_serialPortName),
                MqttAvailable: false,
                SerialPort: _serialPortName,
                AvailablePorts: availablePorts
            );
        }
    }
}
