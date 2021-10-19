using Newtonsoft.Json;
using RomBuilder.Serialization;

namespace RomBuilder.Config
{
    public class Rom
    {
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint Size { get; set; }

        public string Filename { get; set; }
    }
}
