using Newtonsoft.Json;
using RomBuilder.Serialization;

namespace RomBuilder.Config
{
    public class Image
    {
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint Offset { get; set; }

        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint Size { get; set; }

        public string Filename { get; set; }

    }
}
