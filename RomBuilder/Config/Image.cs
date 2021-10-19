using Newtonsoft.Json;
using RomBuilder.Serialization;

namespace RomBuilder.Config
{
    public class Image : Rom
    {
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint Offset { get; set; }
    }
}
