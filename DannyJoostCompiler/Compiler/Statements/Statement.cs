using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public class Statement
	{
		protected LinkedList<Node> compiledStatement;

		public Statement ()
		{
			compiledStatement = new LinkedList<Node>();
		}

		public abstract LinkedList<Node> Compile (ref LinkedListNode<Token> currentToken);
	}
}

