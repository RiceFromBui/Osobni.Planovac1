using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Osobni.Planovac1
{
    public static class EventStorage
    {
        private const string FILE_NAME = "events.json";

        // Třída pro práci s událostmi uloženými v JSON souboru.
        public static Dictionary<string, Dictionary<string, EventModel>> LoadAll()
        {
            if (!File.Exists(FILE_NAME))
                return new Dictionary<string, Dictionary<string, EventModel>>();

            string json = File.ReadAllText(FILE_NAME);
            try
            {
                return JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, EventModel>>>(json)
                       ?? new Dictionary<string, Dictionary<string, EventModel>>();
            }
            catch
            {
                return new Dictionary<string, Dictionary<string, EventModel>>();  //Kdyby byl soubor poškozený
            }
        }
        
        public static void SaveAll(Dictionary<string, Dictionary<string, EventModel>> allData) //Uloží všechny události do souboru
        {
            string json = JsonSerializer.Serialize(allData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FILE_NAME, json);
        }

        public static void SaveEvent(DateTime date, string time, EventModel eventData) //Exportuje události jendoho dne do souboru
        {
            var all = LoadAll();
            string key = date.ToString("yyyy-MM-dd");

            if (!all.ContainsKey(key))
                all[key] = new Dictionary<string, EventModel>();

            if (string.IsNullOrWhiteSpace(eventData.Text))
            {
                if (all[key].ContainsKey(time))
                    all[key].Remove(time);
            }
            else
            {
                all[key][time] = eventData;
            }

            // Úklid prázdných dní
            if (all[key].Count == 0)
                all.Remove(key);

            SaveAll(all);
        }
        public static void ExportDay(DateTime date, string path)
        {
            var all = LoadAll();
            string key = date.ToString("yyyy-MM-dd");

            if (!all.ContainsKey(key)) return;

            var lines = new List<string>();
            foreach (var entry in all[key])
            {
            
                lines.Add($"{entry.Key} [{entry.Value.Category}] - {entry.Value.Text}");
            }

            File.WriteAllLines(path, lines);
        }
    }
}