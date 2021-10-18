using System.Threading.Tasks;

namespace RomBuilder
{
    interface IApplication
    {
        Task Run(string[] args);
    }
}
