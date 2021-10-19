using System;
using System.CommandLine;
using System.CommandLine.Invocation;
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
        }
    }
}
