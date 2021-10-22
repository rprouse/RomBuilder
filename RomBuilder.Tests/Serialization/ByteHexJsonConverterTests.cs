using System;
using FluentAssertions;
using NUnit.Framework;
using RomBuilder.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RomBuilder.Tests.Serialization
{
    [TestFixture]
    public class ByteHexJsonConverterTests
    {
        [Test]
        public void CanSerializeByteHexString()
        {
            var value = new TestByteObject { Value = 0xE0 };
            var result = JsonSerializer.Serialize(value);
            result.Should().Contain("0xE0");
        }

        [TestCase("0xE0", 0xE0)]
        [TestCase("0xe0", 0xE0)]
        [TestCase("0xFf", 0xFF)]
        public void CanDeserializeByteHexString(string value, byte expected)
        {
            var json = $"{{ \"Value\":\"{value}\" }}";
            var result = JsonSerializer.Deserialize<TestByteObject>(json);
            result.Value.Should().Be(expected);
        }

        [TestCase("E0")]
        [TestCase("12")]
        [TestCase("")]
        [TestCase("string")]
        [TestCase(null)]
        public void ThrowsJsonSerializationExceptionOnInvalidValue(string value)
        {
            var test = $"{{ \"Value\":\"{value}\" }}";
            Action action = () => JsonSerializer.Deserialize<TestByteObject>(test);

            action.Should()
                .Throw<JsonException>()
                .WithMessage("Value must be in the format 0xFF");
        }

        [Test]
        public void ThrowsJsonSerializationExceptionOnInvalidPropertyType()
        {
            var test = "{ \"Value\":\"0xFF\" }";
            Action action = () => JsonSerializer.Deserialize<TestInvalidObject>(test);

            action.Should()
                .Throw<InvalidOperationException>();
        }

        class TestByteObject
        {
            [JsonConverter(typeof(ByteHexJsonConverter))]
            public byte Value { get; set; }
        }

        class TestInvalidObject
        {
            [JsonConverter(typeof(ByteHexJsonConverter))]
            public string Value { get; set; }
        }
    }
}
