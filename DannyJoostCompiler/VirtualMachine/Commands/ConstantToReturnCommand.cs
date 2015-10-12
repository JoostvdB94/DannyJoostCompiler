using System;
using System.Collections.Generic;
using DannyJoostCompiler.VirtualMachine.Commands;

namespace DannyJoostCompiler.VirtualMachine
{
    internal class ConstantToReturnCommand : BaseCommand
    {
        public void Execute(UltimateVirtualMachine vm, List<Token> parameters)
        {
            vm.ReturnValue.Value = parameters[0].Value;
            vm.ReturnValue.type = parameters[0].Type;
        }
    }
}