using System.IO;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using RomBuilder.Config;

namespace RomBuilder.Tests.Config
{
    [TestFixture]
    public class RomConfigFileTests
    {
        string _configFile;

           [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string solutionPath = Directory
                .GetParent(asm.Location)
                .Parent.Parent.Parent.Parent.FullName;

            _configFile = Path.Combine(solutionPath, "Samples", "RC2014", "24886009.json");

            Assume.That(File.Exists(_configFile));
        }

        [Test]
        public void CanReadRomConfigFiles()
        {
            var config = RomConfigFile.Read(_configFile);
            config.Should().NotBeNull();
            config.Rom.Default.Should().Be(0xFF);
            config.Rom.Size.Should().Be(0x10000);
            config.Images.Should().HaveCount(5);
            config.Images[0].Filename.Should().Be("imgs/2-MBasic_32K_SIO2.bin");
        }

        [Test]
        public void ReadMissingFileReturnsNull()
        {
            var result = RomConfigFile.Read("fake.json");
            result.Should().BeNull();
        }
    }
}
