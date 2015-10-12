using System;
using System.Collections.Generic;
using DannyJoostCompiler.DictionaryExtension;

namespace DannyJoostCompiler
{
    public static class NodeFactory
    {
        public static Dictionary<string, Node> Nodes { private get; set; }
        static NodeFactory() {
            SetupNodes();
        }

        public static Node create(string nodeType, string identifier = "", List<string> parameters = null)
        {
            var node = Nodes.GetValue(nodeType);

            if (node != null)
            {
                Node copiedNode = node.Copy();
                copiedNode.Identifier = identifier;
                copiedNode.SetupParameters(parameters);
                return copiedNode;
            }
            throw new KeyNotFoundException("No class found with the name "
                + nodeType);
        }

        private static void SetupNodes ()
		{
            /* Deze kunnen niet ivm parameters, toch?
			nodes.Add ("DirectFunctionCall", DirectFunctionCall);
			nodes.Add ("FunctionCall", FunctionCall);
			*/
            Nodes.Add("DoNothing", new DoNothingNode());
            Nodes.Add("DirectFunctionCall", new DirectFunctionCall());
            Nodes.Add("FunctionCall", new FunctionCall());
        }
	}
}

