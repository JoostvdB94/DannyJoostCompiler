using System;
using System.IO;
using System.Text;


namespace DannyJoostCompiler
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Tokenizer tokenizer = new Tokenizer();
            var lines = (File.ReadAllLines(Environment.CurrentDirectory + @"/Language.txt"));
            foreach (var item in tokenizer.Tokenize(lines))
            {
               Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
		}
	}
}
