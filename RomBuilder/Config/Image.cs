using System.Text.Json.Serialization;
using RomBuilder.Serialization;

namespace RomBuilder.Config
{
    public class Image : BaseImage
    {
        [JsonConverter(typeof(UInt32HexJsonConverter))]
        public uint Offset { get; set; }
    }
}
