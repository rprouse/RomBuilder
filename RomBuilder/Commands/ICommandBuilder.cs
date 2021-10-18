using System.CommandLine;

namespace RomBuilder.Commands
{
    interface ICommandBuilder
    {
        Command GetCommand();
    }
}
