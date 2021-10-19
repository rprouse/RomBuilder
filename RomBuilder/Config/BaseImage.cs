using Newtonsoft.Json;
using RomBuilder.Serialization;

namespace RomBuilder.Config
{
    public class BaseImage
    {
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint Size { get; set; }

        public string Filename { get; set; }
    }
}
