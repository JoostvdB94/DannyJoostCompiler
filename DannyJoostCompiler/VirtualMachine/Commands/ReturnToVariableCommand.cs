using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DannyJoostCompiler.DictionaryExtension;

namespace DannyJoostCompiler.VirtualMachine.Commands
{
	public class ReturnToVariableCommand : BaseCommand
	{
		public void Execute (UltimateVirtualMachine vm, List<Token> parameters)
		{
			if (vm.Variables.ContainsKey (parameters [0].Value)) {
				vm.Variables [parameters [0].Value].Value = vm.ReturnValue.Value;
			} else {
				vm.Variables.Add (parameters [0].Value, vm.ReturnValue.Copy ());
			}
		}
	}
}
