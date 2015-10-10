using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public class NodeFactory
	{
		private Dictionary<string,Type> nodes;
		public NodeFactory ()
		{
			setupNodes ();
		}

		public static Node create(string nodeType){
			Type result = null;
			this.nodes.TryGetValue (nodeType, out result);

			if (result != null) {
				return (Statement)Activator.CreateInstance (result);
			}

			return null;
		}

		private void setupNodes ()
		{
			/* Deze kunnen niet ivm parameters, toch?
			nodes.Add ("DirectFunctionCall", DirectFunctionCall);
			nodes.Add ("FunctionCall", FunctionCall);
			*/
			nodes.Add ("DoNothing", DoNothingNode);
		}
	}
}

