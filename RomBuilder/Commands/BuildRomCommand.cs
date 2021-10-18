using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace RomBuilder.Commands
{
    class BuildRomCommand : ICommandBuilder
    {
        public Command GetCommand()
        {
            var command = new Command("build", "Builds a ROM from several hex image files")
            {
                new Argument<string>("config", "ROM configuration/layout file"),
                new Argument<string>("rom", "ROM file to build")
            };
            command.Handler = CommandHandler.Create((string config, string rom) => Execute(config, rom));
            return command;
        }

        void Execute(string config, string rom)
        {
            Console.WriteLine($"Building {rom} with {config}");
        }
    }
}
