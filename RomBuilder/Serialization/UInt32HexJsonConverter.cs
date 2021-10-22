using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RomBuilder.Serialization
{
    public sealed class UInt32HexJsonConverter : JsonConverter<uint>
    {
        public override uint Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if (str == null || !str.StartsWith("0x"))
                throw new JsonException("Value must be in the format 0xFF");
            
            return Convert.ToUInt32(str, 16);
        }

        public override void Write(Utf8JsonWriter writer, uint value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"0x{value:X}");
        }
    }
}
