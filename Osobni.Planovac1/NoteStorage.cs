using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Osobni.Planovac1
{
    public static class NoteStorage
    {
        private static readonly string filePath = "notes.json";

        public static Dictionary<string, string> LoadNotes()
        {
            if (!File.Exists(filePath)) return new Dictionary<string, string>();
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
        }

        public static void SaveNote(DateTime date, string note)
        {
            var notes = LoadNotes();
            string key = date.ToString("yyyy-MM-dd");

            if (string.IsNullOrWhiteSpace(note))
            {
                notes.Remove(key); // 🧹 smaž poznámku
            }
            else
            {
                notes[key] = note; // ✍️ ulož poznámku
            }

            string json = JsonSerializer.Serialize(notes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }


        public static string GetNoteForDate(DateTime date)
        {
            var notes = LoadNotes();
            string key = date.ToString("yyyy-MM-dd");
            return notes.TryGetValue(key, out string note) ? note : string.Empty;
        }
    }
}
