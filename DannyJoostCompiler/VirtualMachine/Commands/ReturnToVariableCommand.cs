using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyJoostCompiler.VirtualMachine.Commands
{
    public class ReturnToVariableCommand : BaseCommand
    {
        public void Execute(UltimateVirtualMachine vm, List<Token> parameters)
        {
            vm.Variables.Add(parameters[0].Value, vm.ReturnValue.Copy());
        }
    }
}
