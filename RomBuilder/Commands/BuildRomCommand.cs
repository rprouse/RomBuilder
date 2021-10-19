using System;
using System.CommandLine;
using System.CommandLine.Invocation;
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
            Console.WriteLine($"Building with {config}");
            var romConfig = RomConfigFile.Read(config);
            if (romConfig == null) return;
        }
    }
}
