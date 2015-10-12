using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public class WhileStatement:Statement
	{
		private LinkedList<Node> condition;
		private LinkedList<Node> body;

		public WhileStatement ()
		{
			condition = new LinkedList<Node> ();
			body = new LinkedList<Node> ();

			compiledStatement.AddLast(NodeFactory.create("DoNothing"));
            foreach(var node in condition)
            {
                compiledStatement.AddLast(node);
            }
			compiledStatement.AddLast(NodeFactory.create("ConditionalJump")); // De body komt dus rechtstreeks na de conditionalJumpNode (dus op de .Next property)
            foreach (var node in body)
            {
                compiledStatement.AddLast(node);
            }
            compiledStatement.AddLast(NodeFactory.create("JumpBack"));
			compiledStatement.AddLast(NodeFactory.create("DoNothing"));
		}

		public override LinkedList<Node> Compile (LinkedListNode<Token> currentToken)
		{

			int whileLevel = currentToken.Value.Level;
			List<TokenExpectation> expected = new List<TokenExpectation>()
			{
				new TokenExpectation(whileLevel, TokenEnumeration.While), 
				new TokenExpectation(whileLevel, TokenEnumeration.OpenEllips),
				new TokenExpectation(whileLevel + 1, TokenEnumeration.Unknown), // Condition
				new TokenExpectation(whileLevel, TokenEnumeration.CloseEllips),
				new TokenExpectation(whileLevel, TokenEnumeration.OpenBracket), 
				new TokenExpectation(whileLevel + 1, TokenEnumeration.Unknown), // Body
				new TokenExpectation(whileLevel, TokenEnumeration.ClosedBracket)
			};

            return null;
		}
	}
}

