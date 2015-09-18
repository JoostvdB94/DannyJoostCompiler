using System;

namespace DannyJoostCompiler.Statements
{
	public class WhileStatement:BaseStatement
	{
		public WhileStatement ()
		{

		}

		public override void identify(){
			Console.WriteLine ("I'm a WhileStatement");
		}
	}
}

