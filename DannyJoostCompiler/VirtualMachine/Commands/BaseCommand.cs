using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyJoostCompiler.VirtualMachine.Commands
{
    public interface BaseCommand
    {
        void Execute(UltimateVirtualMachine vm, List<Token> parameters);
    }
}
