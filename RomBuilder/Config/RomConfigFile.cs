using System;
using System.IO;
using Newtonsoft.Json;
using RomBuilder.Serialization;

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
                return JsonConvert.DeserializeObject<RomConfigFile>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read {config}, {ex.Message}.");
                return null;
            }
        }

        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint Size { get; set; }

        public string Filename { get; set; }

        public Image[] Images { get; set; }
    }
}
