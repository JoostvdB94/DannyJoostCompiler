using System;

namespace DannyJoostCompiler.Statements
{
	public class EmptyStatement:BaseStatement
	{
		public EmptyStatement ()
		{
			Console.WriteLine ("WARNING! You may have a typo somewhere!");
		}

		public override void identify ()
		{
			Console.Write ("I'm a Empty Statement.. Use me for non-existant classes!");
		}

		#region implemented abstract members of BaseStatement

		public override CharEnumerator SetupParameters (CharEnumerator inputEnummerator)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

