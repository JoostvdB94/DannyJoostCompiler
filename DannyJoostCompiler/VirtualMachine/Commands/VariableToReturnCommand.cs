using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyJoostCompiler.VirtualMachine.Commands
{
    public class VariableToReturnCommand : BaseCommand
    {
        public void Execute(UltimateVirtualMachine vm, List<Token> parameters)
        {
            if(vm.Variables.ContainsKey(parameters[0].Value))
            {
                vm.ReturnValue.Value = vm.Variables[parameters[0].Value].Value;
                TokenEnumeration type = vm.Variables[parameters[0].Value].type;
                switch (vm.Variables[parameters[0].Value].type)
                {
                    case TokenEnumeration.Integer:
                        type = TokenEnumeration.Number;
                        break;
                    case TokenEnumeration.String:
                        type = TokenEnumeration.QuotedString;
                        break;
                }
                vm.ReturnValue.type = type;
            } else
            {
                Console.WriteLine("Variable " + parameters[0].Value + " was not declared");
            }
        }
    }
}
