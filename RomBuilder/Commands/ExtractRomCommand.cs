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

            if(!File.Exists(romConfig.Rom.Filename))
            {
                Console.WriteLine($"The ROM {romConfig.Rom.Filename} does not exist.");
                return;
            }

            Console.WriteLine($"Reading ROM {romConfig.Rom.Filename}");
            byte[] rom = File.ReadAllBytes(romConfig.Rom.Filename);
            if(rom.Length != romConfig.Rom.Size)
            {
                Console.WriteLine($"ROM is {rom.Length} bytes but config is {romConfig.Rom.Size} bytes.");
                return;
            }

            foreach(var image in romConfig.Images)
            {
                // Create the directory if it doesn't exist
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
