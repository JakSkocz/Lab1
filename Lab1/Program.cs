using System;
using System.Collections.Generic;
using TemperatureSensorApp.Models;
using TemperatureSensorApp.Utils;

namespace TemperatureSensorApp
{
    class Program
    {
        static void Main()
        {
            AdvancedTemperatureSensor sensor = new AdvancedTemperatureSensor();

            // 1. Generowanie listy temperatur
            Console.WriteLine("Podaj liczbę temperatur do wygenerowania:");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int n) || n <= 0)
            {
                Console.WriteLine("Podano niepoprawną liczbę. Program zostanie zakończony.");
                return;
            }

            List<double?> temperatureList = sensor.GetTemperatureList(n);
            TemperatureFormatter.DisplayTemperatures(temperatureList, "Wygenerowane temperatury:");

            // 2. Zapis listy do pliku
            sensor.SaveTemperatureListToFile(temperatureList);

            // 3. Obliczanie średniej
            double average = sensor.CalculateAverage(temperatureList);
            Console.WriteLine($"\nŚrednia temperatur: {average:F2}°C");

            // 4. Obliczanie odchylenia standardowego
            double standardDeviation = sensor.CalculateStandardDeviation(temperatureList);
            Console.WriteLine($"Odchylenie standardowe: {standardDeviation:F2}");

            // 5. Sortowanie listy i zapis do pliku
            List<double?> sortedTemperatures = sensor.SortTemperatures(temperatureList);
            TemperatureFormatter.DisplayTemperatures(sortedTemperatures, "\nPosortowane temperatury:");
            sensor.SaveSortedTemperaturesToFile(sortedTemperatures);

            // 6. Usuwanie wartości z zakresu i sortowanie
            Console.WriteLine("\nPodaj minimalną wartość zakresu do usunięcia:");
            if (!double.TryParse(Console.ReadLine(), out double minValue))
            {
                Console.WriteLine("Niepoprawna wartość minimalna. Program zostanie zakończony.");
                return;
            }

            Console.WriteLine("Podaj maksymalną wartość zakresu do usunięcia:");
            if (!double.TryParse(Console.ReadLine(), out double maxValue))
            {
                Console.WriteLine("Niepoprawna wartość maksymalna. Program zostanie zakończony.");
                return;
            }

            List<double?> filteredTemperatures = sensor.RemoveValuesInRange(temperatureList, minValue, maxValue);
            TemperatureFormatter.DisplayTemperatures(filteredTemperatures, "\nTemperatury po usunięciu wartości z zakresu i posortowaniu:");
            sensor.SaveFilteredTemperaturesToFile(filteredTemperatures);

            // 7. Odczyt najnowszego pliku i opcjonalne usunięcie
            Console.WriteLine("\nCzy chcesz usunąć plik po odczycie? (tak/nie)");
            string deleteAfterReadInput = Console.ReadLine();
            bool deleteAfterRead = deleteAfterReadInput.Equals("tak", StringComparison.OrdinalIgnoreCase);

            List<double?> latestTemperatures = sensor.ReadLatestTemperatureFile(deleteAfterRead);
            if (latestTemperatures.Count > 0)
            {
                TemperatureFormatter.DisplayTemperatures(latestTemperatures, "\nOdczytane temperatury z najnowszego pliku:");
            }
        }
    }
}
