using System;
using System.IO;
using System.Text;


namespace DannyJoostCompiler
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Tokenizer tokenizer = new Tokenizer (new StreamReader(Environment.CurrentDirectory + @"\Language.txt"));
			tokenizer.tokenize ();
            Console.ReadKey();
		}
	}
}
