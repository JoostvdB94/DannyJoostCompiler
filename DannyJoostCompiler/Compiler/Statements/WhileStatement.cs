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
			compiledStatement.AddLast(condition);
			compiledStatement.AddLast(NodeFactory.create("ConditionalJump")); // De body komt dus rechtstreeks na de conditionalJumpNode (dus op de .Next property)
			compiledStatement.AddLast(body);
			compiledStatement.AddLast(NodeFactory.create("JumpBack"));
			compiledStatement.AddLast(NodeFactory.create("DoNothing"));
		}

		public override LinkedList<Node> Compile (ref LinkedListNode<Token> currentToken)
		{

			int whileLevel = currentToken.Value.Level;
			List<TokenExpectation> expected = new List<TokenExpectation>()
			{
				new TokenExpectation(whileLevel, TokenEnumeration.While), 
				new TokenExpectation(whileLevel, TokenEnumeration.OpenEllips),
				new TokenExpectation(whileLevel + 1, TokenEnumeration.ANY), // Condition
				new TokenExpectation(whileLevel, TokenEnumeration.CloseEllips),
				new TokenExpectation(whileLevel, TokenEnumeration.OpenBracket), 
				new TokenExpectation(whileLevel + 1, TokenEnumeration.ANY), // Body
				new TokenExpectation(whileLevel, TokenEnumeration.BRACKET_CLOSE)
			};
		}
	}
}

