using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using RomBuilder.Commands;

namespace RomBuilder
{
    class Application : IApplication
    {
        readonly Parser _parser;
        readonly IEnumerable<ICommandBuilder> _commandBuilders;

        public Application(IEnumerable<ICommandBuilder> commandBuilders)
        {
            _commandBuilders = commandBuilders;

            var rootCommand = new RootCommand(AssemblyDescription);
            foreach (var command in _commandBuilders.Select(b => b.GetCommand()))
                rootCommand.AddCommand(command);

            _parser = new CommandLineBuilder(rootCommand)
                .UseDefaults()
                .Build();
        }

        public async Task Run(string[] args)
        {
            await _parser.InvokeAsync(args).ConfigureAwait(false);
        }

        private string AssemblyDescription =>
            Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)
                .OfType<AssemblyDescriptionAttribute>()
                .FirstOrDefault()
                ?.Description ?? "";
    }
}
