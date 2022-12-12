using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AdvancedFunctions
{
    public class SaveFormedFilesSettings
    {
        public string FilePath { get; set; }
        public bool Archiving { get; set; }
    }

    public static class SaveFormedFilesSettingsManager
    {
        public static SaveFormedFilesSettings Load()
        {
            SaveFormedFilesSettings sffs = new SaveFormedFilesSettings() {
                FilePath = $"{Environment.CurrentDirectory}\\Results",
                Archiving = false
            };

            try
            {
                var settingsPath = $"{Environment.CurrentDirectory}\\pathSettings.json";
                if (File.Exists(settingsPath))
                {
                    using (var fs = new FileStream(settingsPath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                    {
                        using (var sr = new StreamReader(fs))
                        {
                            var sffsStr = sr.ReadToEnd();
                            sffs = JsonConvert.DeserializeObject<SaveFormedFilesSettings>(sffsStr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InformOperations.setDisplayMessage($"Ошибка при загрузке пути для сформированных файлов: {ex.Message}");
            }
            return sffs;
        }

        public static void Save(SaveFormedFilesSettings sffs)
        {
            try
            {
                if (sffs != null)
                {
                    File.WriteAllText($"{Environment.CurrentDirectory}\\pathSettings.json", string.Empty);
                    var sffsStr = JsonConvert.SerializeObject(sffs, Formatting.Indented);
                    using (var fs = new FileStream($"{Environment.CurrentDirectory}\\pathSettings.json", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                    {
                        using (var sw = new StreamWriter(fs))
                        {
                            sw.Write(sffsStr);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                InformOperations.setDisplayMessage($"Ошибка при сохранении пути для сформированных файлов: {ex.Message}");
            }
        }
    }
}
