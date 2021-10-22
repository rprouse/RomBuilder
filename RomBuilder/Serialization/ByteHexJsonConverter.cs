using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RomBuilder.Serialization
{
    public sealed class ByteHexJsonConverter : JsonConverter<byte>
    {
        public override byte Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if (str == null || !str.StartsWith("0x"))
                throw new JsonException("Value must be in the format 0xFF");

            return Convert.ToByte(str, 16);
        }

        public override void Write(Utf8JsonWriter writer, byte value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"0x{value:X}");
        }
    }
}
