using System.Text.Json.Serialization;
using RomBuilder.Serialization;

namespace RomBuilder.Config
{
    public class Rom : BaseImage
    {
        /// <summary>
        /// The default fill character for the ROM, often set to a NOP
        /// </summary>
        [JsonConverter(typeof(ByteHexJsonConverter))]
        public byte Default { get; set; }
    }
}
