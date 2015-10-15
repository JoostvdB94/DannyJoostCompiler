using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DannyJoostCompiler.Compiler;
using DannyJoostCompiler.Datastructures;
using DannyJoostCompiler.VirtualMachine;

namespace DannyJoostCompiler
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var lines = File.ReadAllLines (Environment.CurrentDirectory + @"/Language.txt");
			Tokenizer tokenizer = new Tokenizer ();
			LinkedList<Token> tokens = tokenizer.Tokenize(lines);

			foreach (var token in tokens) {
			//	Console.WriteLine (token.ToString ());
			}
			//Console.ReadKey();

			NodeCompiler compiler = new NodeCompiler ();
			DoubleLinkedList nodes = compiler.CompileLinkedList (tokens);

			UltimateVirtualMachine vm = new UltimateVirtualMachine ();
			vm.Run (nodes);
		}
	}
}
