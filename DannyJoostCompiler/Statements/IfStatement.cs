using System;

namespace DannyJoostCompiler.Statements
{
	public class IfStatement:BaseStatement
	{
		public IfStatement ()
		{
		}

		public override void identify ()
		{
			Console.WriteLine ("I'm a If statement");
		}

		#region implemented abstract members of BaseStatement

		public override CharEnumerator SetupParameters (CharEnumerator inputEnummerator)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

