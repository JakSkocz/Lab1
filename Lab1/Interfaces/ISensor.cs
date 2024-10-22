using System.Collections.Generic;

namespace TemperatureSensorApp.Interfaces
{
    public interface ISensor
    {
        List<double?> GetTemperatureList(int n);
        void SaveTemperatureListToFile(List<double?> temperatures);
        List<double?> ReadLatestTemperatureFile(bool deleteAfterRead);
    }
}
