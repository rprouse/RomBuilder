using System;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RomBuilder.Serialization;

namespace RomBuilder.Tests.Serialization
{
    [TestFixture]
    public class HexStringJsonConverterTests
    {
        [Test]
        public void CanSerializeHexString()
        {
            var value = new TestObject { Value = 0xE000 };
            var result = JsonConvert.SerializeObject(value);
            result.Should().Contain("0xE000");
        }

        [TestCase("0xE000", 0xE000u)]
        [TestCase("0xe000", 0xE000u)]
        [TestCase("0xFFff", 0xFFFFu)]
        public void CanDeserializeHexString(string value, uint expected)
        {
            var json = $"{{ \"Value\":\"{value}\" }}";
            var result = JsonConvert.DeserializeObject<TestObject>(json);
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
            Action action = () => JsonConvert.DeserializeObject<TestObject>(test);

            action.Should()
                .Throw<JsonSerializationException>()
                .WithMessage("Value must be in the format 0xFF");
        }

        class TestObject
        {
            [JsonConverter(typeof(HexStringJsonConverter))]
            public uint Value { get; set; }
        }
    }
}
