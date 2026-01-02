// File: EventStorage.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Osobni.Planovac1
{
    public static class EventStorage
    {
        private static readonly string filePath = "events.json";

        public static Dictionary<string, Dictionary<string, string>> LoadAll()
        {
            if (!File.Exists(filePath))
                return new();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json)
                   ?? new();
        }

        public static void SaveAll(Dictionary<string, Dictionary<string, string>> allData)
        {
            string json = JsonSerializer.Serialize(allData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public static void SaveAllForDate(DateTime date, Dictionary<string, string> entries)
        {
            var all = LoadAll();
            string key = date.ToString("yyyy-MM-dd");
            if (entries.Count > 0)
                all[key] = entries;
            else
                all.Remove(key);
            SaveAll(all);
        }

        public static string GetNoteForTime(DateTime date, string time)
        {
            var all = LoadAll();
            string key = date.ToString("yyyy-MM-dd");
            if (all.TryGetValue(key, out var entries))
            {
                if (entries.TryGetValue(time, out var note))
                    return note;
            }
            return string.Empty;
        }
    
    public static void ExportDay(DateTime date, string path)
        {
            var all = LoadAll();
            string key = date.ToString("yyyy-MM-dd");
            if (!all.ContainsKey(key))
                return;

            var lines = new List<string>();
            foreach (var entry in all[key])
            {
                lines.Add($"{entry.Key} - {entry.Value}");
            }

            File.WriteAllLines(path, lines);
        }

        public static void ExportMonth(int year, int month, string path)
        {
            var all = LoadAll();
            var lines = new List<string>();

            foreach (var kvp in all)
            {
                if (DateTime.TryParse(kvp.Key, out var date))
                {
                    if (date.Year == year && date.Month == month)
                    {
                        foreach (var entry in kvp.Value)
                        {
                            lines.Add($"{kvp.Key} {entry.Key} - {entry.Value}");
                        }
                    }
                }
            }

            File.WriteAllLines(path, lines);
        }

    }
}