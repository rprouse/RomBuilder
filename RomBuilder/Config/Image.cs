using Newtonsoft.Json;
using RomBuilder.Serialization;

namespace RomBuilder.Config
{
    public class Image : BaseImage
    {
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint Offset { get; set; }
    }
}
