using System;
using System.Collections.Generic;
using DannyJoostCompiler.VirtualMachine.Commands;

namespace DannyJoostCompiler.VirtualMachine
{
    public class DeclareVariableTypeCommand : BaseCommand
    {
        public void Execute(UltimateVirtualMachine vm, List<Token> parameters)
        {
            if (vm.Variables.ContainsKey(parameters[1].Value))
            {
                Console.WriteLine(parameters[1].Value + " was allready declared!");
            }
            else
            {
                vm.Variables.Add(parameters[1].Value, new Variable() { Value = 0, type = parameters[0].Type });
            }
           
        }
    }
}