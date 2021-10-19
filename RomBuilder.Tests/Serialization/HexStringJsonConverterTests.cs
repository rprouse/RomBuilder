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
        public void CanSerializeUintHexString()
        {
            var value = new TestUintObject { Value = 0xE000 };
            var result = JsonConvert.SerializeObject(value);
            result.Should().Contain("0xE000");
        }

        [TestCase("0xE000", 0xE000u)]
        [TestCase("0xe000", 0xE000u)]
        [TestCase("0xFFff", 0xFFFFu)]
        public void CanDeserializeUintHexString(string value, uint expected)
        {
            var json = $"{{ \"Value\":\"{value}\" }}";
            var result = JsonConvert.DeserializeObject<TestUintObject>(json);
            result.Value.Should().Be(expected);
        }

        [Test]
        public void CanSerializeByteHexString()
        {
            var value = new TestByteObject { Value = 0xE0 };
            var result = JsonConvert.SerializeObject(value);
            result.Should().Contain("0xE0");
        }

        [TestCase("0xE0", 0xE0)]
        [TestCase("0xe0", 0xE0)]
        [TestCase("0xFf", 0xFF)]
        public void CanDeserializeByteHexString(string value, byte expected)
        {
            var json = $"{{ \"Value\":\"{value}\" }}";
            var result = JsonConvert.DeserializeObject<TestByteObject>(json);
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
            Action action = () => JsonConvert.DeserializeObject<TestUintObject>(test);

            action.Should()
                .Throw<JsonSerializationException>()
                .WithMessage("Value must be in the format 0xFF");
        }

        [Test]
        public void ThrowsJsonSerializationExceptionOnInvalidPropertyType()
        {
            var test = "{ \"Value\":\"0xFF\" }";
            Action action = () => JsonConvert.DeserializeObject<TestInvalidObject>(test);

            action.Should()
                .Throw<JsonSerializationException>()
                .WithMessage("Properties of System.String are not supported");
        }

        class TestUintObject
        {
            [JsonConverter(typeof(HexStringJsonConverter))]
            public uint Value { get; set; }
        }

        class TestByteObject
        {
            [JsonConverter(typeof(HexStringJsonConverter))]
            public byte Value { get; set; }
        }

        class TestInvalidObject
        {
            [JsonConverter(typeof(HexStringJsonConverter))]
            public string Value { get; set; }
        }
    }
}
