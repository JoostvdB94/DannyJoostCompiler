using System;
using System.Collections.Generic;
using DannyJoostCompiler.DictionaryExtension;
using DannyJoostCompiler.Compiler.Nodes;

namespace DannyJoostCompiler
{
	public static class NodeFactory
	{
		public static Dictionary<string, Node> Nodes { private get; set; }

		static NodeFactory ()
		{
			Nodes = new Dictionary<string, Node> ();
			SetupNodes ();
		}

		public static Node Create (string nodeType, string identifier = "", List<Token> parameters = null)
		{
			var node = Nodes.GetValue (nodeType);

			if (node != null) {
				Node copiedNode = node.Copy ();
				copiedNode.Identifier = identifier;
				copiedNode.SetupParameters (parameters);
				return copiedNode;
			}
			throw new KeyNotFoundException ("No class found with the name "
			+ nodeType);
		}

		private static void SetupNodes ()
		{
			Nodes.Add ("DoNothing", new DoNothingNode ());

			Nodes.Add ("DirectFunctionCall", new DirectFunctionCall ());
			Nodes.Add ("FunctionCall", new FunctionCall ());

			Nodes.Add ("Jump", new JumpNode ());
			Nodes.Add ("ConditionalJump", new ConditionalJumpNode ());
     
		}
	}
}

