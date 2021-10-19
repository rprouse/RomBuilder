using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using RomBuilder.Config;

namespace RomBuilder.Commands
{
    class BuildRomCommand : ICommandBuilder
    {
        public Command GetCommand()
        {
            var command = new Command("build", "Builds a ROM from several hex image files")
            {
                new Argument<string>("config", "ROM configuration/layout file")
            };
            command.Handler = CommandHandler.Create((string config) => Execute(config));
            return command;
        }

        void Execute(string config)
        {
            Console.WriteLine($"Building using {config}");
            var romConfig = RomConfigFile.Read(config);
            if (romConfig == null) return;
                
            // Fill the rom with the default byte value
            byte[] bytes = Enumerable
                .Repeat(romConfig.Rom.Default, (int)romConfig.Rom.Size)
                .ToArray();

            foreach(var image in romConfig.Images)
            {
                if(!File.Exists(image.Filename))
                {
                    Console.WriteLine($"The image {image.Filename} does not exist.");
                    return;
                }

                Console.WriteLine($"Reading image {image.Filename}");
                byte[] imageBytes = File.ReadAllBytes(image.Filename);

                if (imageBytes.Length > image.Size)
                {
                    Console.WriteLine($"Image file is {imageBytes.Length} bytes, but size was specified as {image.Size} bytes.");
                    return;
                }

                imageBytes.CopyTo(bytes, image.Offset);
            }

            // Create the directory if it doesn't exist
            FileInfo fi = new FileInfo(romConfig.Rom.Filename);
            if (fi.Directory != null)
                Directory.CreateDirectory(fi.DirectoryName);

            Console.WriteLine($"Writing rom {romConfig.Rom.Filename}");
            File.WriteAllBytes(romConfig.Rom.Filename, bytes);
        }
    }
}
