using System;
using FluentAssertions;
using NUnit.Framework;
using RomBuilder.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RomBuilder.Tests.Serialization
{
    [TestFixture]
    public class UInt32HexJsonConverterTests
    {
        [Test]
        public void CanSerializeUintHexString()
        {
            var value = new TestUintObject { Value = 0xE000 };
            var result = JsonSerializer.Serialize(value);
            result.Should().Contain("0xE000");
        }

        [TestCase("0xE000", 0xE000u)]
        [TestCase("0xe000", 0xE000u)]
        [TestCase("0xFFff", 0xFFFFu)]
        public void CanDeserializeUintHexString(string value, uint expected)
        {
            var json = $"{{ \"Value\":\"{value}\" }}";
            var result = JsonSerializer.Deserialize<TestUintObject>(json);
            result.Value.Should().Be(expected);
        }

        [TestCase("E000")]
        [TestCase("12")]
        [TestCase("")]
        [TestCase("string")]
        [TestCase(null)]
        public void ThrowsJsonSerializationExceptionOnInvalidValue(string value)
        {
            var test = $"{{ \"Value\":\"{value}\" }}";
            Action action = () => JsonSerializer.Deserialize<TestUintObject>(test);

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

        class TestUintObject
        {
            [JsonConverter(typeof(UInt32HexJsonConverter))]
            public uint Value { get; set; }
        }

        class TestInvalidObject
        {
            [JsonConverter(typeof(UInt32HexJsonConverter))]
            public string Value { get; set; }
        }
    }
}
