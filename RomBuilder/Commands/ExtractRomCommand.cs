using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace RomBuilder.Commands
{
    class ExtractRomCommand : ICommandBuilder
    {
        public Command GetCommand()
        {
            var command = new Command("extract", "Extracts hex images from a ROM file")
            {
                new Argument<string>("config", "ROM configuration/layout file"),
                new Argument<string>("rom", "ROM file to extract from")
            };
            command.Handler = CommandHandler.Create((string config, string rom) => Execute(config, rom));
            return command;
        }

        void Execute(string config, string rom)
        {
            Console.WriteLine($"Extracting {rom} with {config}");
        }

    }
}
