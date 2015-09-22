using System;
using System.Collections.Generic;

namespace DannyJoostCompiler.Statements
{
	public class WhileStatement:BaseStatement
	{
		public List<string> Parameters{ get; set;} //TODO change type of list items 
		public WhileStatement ()
		{

		}

		public override void identify(){
			Console.WriteLine ("I'm a WhileStatement");
		}

		public override CharEnumerator SetupParameters (CharEnumerator inputEnummerator)
		{
			string param = "";
			while(inputEnummerator.MoveNext()){
				if (inputEnummerator.Current.Equals (',')) {
					Parameters.Add (param);
					continue;
				}else if (inputEnummerator.Current.Equals (')')) {
					return inputEnummerator;
				}
				param += inputEnummerator.Current;
			}
			return null;
		}
	}
}

