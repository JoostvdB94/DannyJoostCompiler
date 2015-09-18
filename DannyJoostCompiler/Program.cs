using System;
using DannyJoostCompiler.Statements;

namespace DannyJoostCompiler
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//string[] classes = ["while"]
			BaseStatement.create("while").identify();
			BaseStatement.create("vfasjkbljhas").identify();
		}
	}
}
