using System;
using DannyJoostCompiler.VirtualMachine.Commands;
using DannyJoostCompiler.VirtualMachine;
using System.Collections.Generic;
using DannyJoostCompiler.DictionaryExtension;

namespace DannyJoostCompiler
{
	public class PrintCommand : BaseCommand
	{
		public PrintCommand ()
		{
		}

		#region BaseCommand implementation

		public void Execute (UltimateVirtualMachine vm, List<Token> parameters)
		{
			foreach (Token token in parameters) {
				string valueToPrint = "";
				switch (token.Type) {
				case TokenEnumeration.QuotedString: 
					valueToPrint = token.Value;
					break;
				case TokenEnumeration.Number:
					valueToPrint = token.Value;
					break;
				case TokenEnumeration.Identifier:
					if (vm.Variables.ContainsKey (token.Value)) {
						valueToPrint = vm.Variables.GetValue (token.Value).Value.ToString ();
					}
					break;
				}
				Console.WriteLine (valueToPrint);
			}
		}

		#endregion
	}
}

