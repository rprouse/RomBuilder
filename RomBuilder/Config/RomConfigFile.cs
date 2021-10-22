using System;
using System.IO;
using System.Text.Json;

namespace RomBuilder.Config
{
    public class RomConfigFile
    {
        public static RomConfigFile Read(string config)
        {
            if (!File.Exists(config))
            {
                Console.WriteLine($"Configfile {config} does not exist.");
                return null;
            }

            try
            {
                string json = File.ReadAllText(config);
                return JsonSerializer.Deserialize<RomConfigFile>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read {config}, {ex.Message}.");
                return null;
            }
        }

        public Rom Rom { get; set; }

        public Image[] Images { get; set; }
    }
}
