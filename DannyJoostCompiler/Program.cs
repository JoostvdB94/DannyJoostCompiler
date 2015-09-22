using System;
using DannyJoostCompiler.Statements;

namespace DannyJoostCompiler
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Tokenizer tokenizer = new Tokenizer ("while (true){int waarde = 3;}");
			tokenizer.tokenize (tokenizer.Code.GetEnumerator());
		}
	}
}
