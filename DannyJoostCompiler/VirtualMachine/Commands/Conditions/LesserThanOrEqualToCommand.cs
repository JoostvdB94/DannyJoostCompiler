using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DannyJoostCompiler.DictionaryExtension;

namespace DannyJoostCompiler.VirtualMachine.Commands
{
    public class LesserThanOrEqualToCommand : BaseCommand
    {
        public void Execute(UltimateVirtualMachine vm, List<Token> parameters)
        {
            if (!vm.Variables.ContainsKey(parameters[0].Value))
            {
                Console.WriteLine(parameters[0].Value + " is not declared");
                return;
            }

            if (!vm.Variables.ContainsKey(parameters[1].Value))
            {
                Console.WriteLine(parameters[1].Value + " is not declared");
                return;
            }

            if (vm.Variables.GetValue(parameters[0].Value).type == TokenEnumeration.Number && vm.Variables.GetValue(parameters[1].Value).type == TokenEnumeration.Number)
            {
                vm.ReturnValue.Value = Convert.ToInt32(vm.Variables.GetValue(parameters[0].Value).Value) <= Convert.ToInt32(vm.Variables.GetValue(parameters[1].Value).Value);
                vm.ReturnValue.type = TokenEnumeration.Bool;
            }
        }
    }
}
