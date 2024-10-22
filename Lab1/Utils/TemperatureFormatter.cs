using System;
using System.Collections.Generic;

namespace TemperatureSensorApp.Utils
{
    public static class TemperatureFormatter
    {
        public static void DisplayTemperatures(List<double?> temperatures, string header = "Temperatury:")
        {
            Console.WriteLine(header);
            for (int i = 0; i < temperatures.Count; i++)
            {
                var temp = temperatures[i];
                Console.WriteLine($"Temperatura {i + 1}: {(temp.HasValue ? $"{temp.Value:F2}°C" : "Błędny odczyt (null)")}");
            }
        }
    }
}
