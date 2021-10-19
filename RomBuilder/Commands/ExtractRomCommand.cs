using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using RomBuilder.Config;

namespace RomBuilder.Commands
{
    class ExtractRomCommand : ICommandBuilder
    {
        public Command GetCommand()
        {
            var command = new Command("extract", "Extracts hex images from a ROM file")
            {
                new Argument<string>("config", "ROM configuration/layout file")
            };
            command.Handler = CommandHandler.Create((string config) => Execute(config));
            return command;
        }

        void Execute(string config)
        {
            Console.WriteLine($"Extracting using {config}");
            var romConfig = RomConfigFile.Read(config);
            if (romConfig == null) return;

            if(!File.Exists(romConfig.Filename))
            {
                Console.WriteLine($"The ROM {romConfig.Filename} does not exist.");
                return;
            }

            Console.WriteLine($"Reading ROM {romConfig.Filename}");
            byte[] rom = File.ReadAllBytes(romConfig.Filename);
            if(rom.Length != romConfig.Size)
            {
                Console.WriteLine($"ROM is {rom.Length} bytes but config is {romConfig.Size} bytes.");
                return;
            }

            foreach(var image in romConfig.Images)
            {
                FileInfo fi = new FileInfo(image.Filename);
                if (fi.Directory != null)
                    Directory.CreateDirectory(fi.DirectoryName);

                byte[] imageBytes = rom
                    .Skip((int)image.Offset)
                    .Take((int)image.Size)
                    .ToArray();

                Console.WriteLine($"Writing {image.Filename}");
                File.WriteAllBytes(image.Filename, imageBytes);
            }
        }
    }
}
