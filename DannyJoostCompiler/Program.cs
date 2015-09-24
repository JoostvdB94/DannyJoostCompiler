using System;
using System.IO;
using System.Text;


namespace DannyJoostCompiler
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Tokenizer tokenizer = new Tokenizer (new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes("if(){}else if(){}else{}"))));
			tokenizer.tokenize ();
		}
	}
}
