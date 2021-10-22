using System.Text.Json.Serialization;
using RomBuilder.Serialization;

namespace RomBuilder.Config
{
    public class BaseImage
    {
        [JsonConverter(typeof(UInt32HexJsonConverter))]
        public uint Size { get; set; }

        public string Filename { get; set; }
    }
}
