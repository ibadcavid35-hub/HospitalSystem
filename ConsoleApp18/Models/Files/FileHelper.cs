using ConsoleApp18.Models.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp18.Models.Files
{
    public static class FileHelper
    {
        private static JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        public static void SaveData<T>(List<T> data, string fileName) where T : Person
        {
            string filePath = $"{fileName}.json";
            string json = JsonSerializer.Serialize(data, serializerOptions);
            File.WriteAllText(filePath, json);
        }

        public static List<T> LoadData<T>(string fileName) where T : Person
        {
            string filePath = $"{fileName}.json";
            if (!File.Exists(filePath)) { return new List<T>(); }
            string readJson = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(readJson, serializerOptions) ?? new List<T>();
        }
    }
}
