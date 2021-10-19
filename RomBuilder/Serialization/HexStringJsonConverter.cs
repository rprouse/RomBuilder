using System;
using Newtonsoft.Json;

namespace RomBuilder.Serialization
{
    public sealed class HexStringJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) =>
            typeof(uint).Equals(objectType) || typeof(byte).Equals(objectType);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue($"0x{value:X}");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var str = reader.Value as string;
            if (str == null || !str.StartsWith("0x"))
                throw new JsonSerializationException("Value must be in the format 0xFF");
            if (typeof(uint).Equals(objectType))
                return Convert.ToUInt32(str, 16);
            else if (typeof(byte).Equals(objectType))
                return Convert.ToByte(str, 16);

            throw new JsonSerializationException($"Properties of {objectType} are not supported");
        }
    }
}
