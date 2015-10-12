using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public abstract class Statement
	{
		protected LinkedList<Node> compiledStatement;

		public Statement ()
		{
			compiledStatement = new LinkedList<Node>();
		}

		public abstract LinkedList<Node> Compile (LinkedListNode<Token> currentToken);
	}
}

