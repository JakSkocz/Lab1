// Models/AdvancedTemperatureSensor.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TemperatureSensorApp.Interfaces;

namespace TemperatureSensorApp.Models
{
    public class AdvancedTemperatureSensor : ISensor
    {
        private Random random;
        private const string JsonFolder = "JsonFiles"; // Nazwa folderu na pliki JSON

        public AdvancedTemperatureSensor()
        {
            random = new Random();
            // Tworzenie folderu, jeśli nie istnieje
            if (!Directory.Exists(JsonFolder))
            {
                Directory.CreateDirectory(JsonFolder);
            }
        }

        // Implementacja metody generującej losowe temperatury
        public List<double?> GetTemperatureList(int n)
        {
            List<double?> temperatures = new List<double?>();
            for (int i = 0; i < n; i++)
            {
                double temperature = random.NextDouble() * 200 - 100;
                temperatures.Add(temperature < -80 ? (double?)null : temperature);
            }
            return temperatures;
        }

        // Implementacja metody zapisującej temperatury do pliku
        public void SaveTemperatureListToFile(List<double?> temperatures)
        {
            string fileName = Path.Combine(JsonFolder, $"{DateTime.Now:yyyy_MM_dd_HH_mm_ss}.json");
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(temperatures, options);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine($"Lista temperatur została zapisana do pliku: {fileName}");
        }

        // Implementacja metody odczytującej najnowszy plik i opcjonalnie go usuwającej
        public List<double?> ReadLatestTemperatureFile(bool deleteAfterRead)
        {
            var files = Directory.GetFiles(JsonFolder, "*.json");

            if (files.Length == 0)
            {
                Console.WriteLine("Brak plików do odczytu.");
                return new List<double?>();
            }

            var latestFile = files.OrderByDescending(f => File.GetLastWriteTime(f)).First();
            string jsonString = File.ReadAllText(latestFile);
            var temperatures = JsonSerializer.Deserialize<List<double?>>(jsonString);

            if (deleteAfterRead)
            {
                File.Delete(latestFile);
                Console.WriteLine($"Plik {latestFile} został usunięty.");
            }

            return temperatures;
        }

        // Obliczanie średniej z listy wartości temperatur (pomijając null)
        public double CalculateAverage(List<double?> temperatures)
        {
            var validTemperatures = temperatures.Where(t => t.HasValue).Select(t => t.Value).ToList();
            return validTemperatures.Count == 0 ? 0 : validTemperatures.Average();
        }

        // Obliczanie odchylenia standardowego z listy wartości temperatur (pomijając null)
        public double CalculateStandardDeviation(List<double?> temperatures)
        {
            var validTemperatures = temperatures.Where(t => t.HasValue).Select(t => t.Value).ToList();
            if (validTemperatures.Count == 0) return 0;

            double average = validTemperatures.Average();
            double sumOfSquares = validTemperatures.Sum(t => Math.Pow(t - average, 2));
            return Math.Sqrt(sumOfSquares / validTemperatures.Count);
        }

        // Sortowanie listy temperatur rosnąco
        public List<double?> SortTemperatures(List<double?> temperatures)
        {
            return temperatures.OrderBy(t => t).ToList();
        }

        // Zapis posortowanej listy do pliku
        public void SaveSortedTemperaturesToFile(List<double?> sortedTemperatures)
        {
            string fileName = Path.Combine(JsonFolder, $"{DateTime.Now:yyyy_MM_dd_HH_mm_ss}_sorted.json");
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(sortedTemperatures, options);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine($"Posortowana lista temperatur została zapisana do pliku: {fileName}");
        }

        // Usuwanie wartości z określonego zakresu i zwracanie posortowanej listy
        public List<double?> RemoveValuesInRange(List<double?> temperatures, double minValue, double maxValue)
        {
            var filteredAndSortedList = temperatures
                .Where(t => !t.HasValue || t < minValue || t > maxValue)
                .OrderBy(t => t)
                .ToList();

            return filteredAndSortedList;
        }

        // Zapis listy z usuniętymi wartościami do pliku
        public void SaveFilteredTemperaturesToFile(List<double?> filteredTemperatures)
        {
            string fileName = Path.Combine(JsonFolder, $"{DateTime.Now:yyyy_MM_dd_HH_mm_ss}_filtered_sorted.json");
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(filteredTemperatures, options);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine($"Lista po usunięciu wartości z zakresu i posortowaniu została zapisana do pliku: {fileName}");
        }
    }
}
