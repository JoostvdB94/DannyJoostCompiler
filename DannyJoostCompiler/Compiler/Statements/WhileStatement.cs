using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public class WhileStatement:Statement
	{
		private DoubleLinkedList condition;
		private DoubleLinkedList body;

		public WhileStatement ()
		{
			condition = new DoubleLinkedList ();
			body = new DoubleLinkedList ();

			compiledStatement.AddLast(NodeFactory.Create("DoNothing"));

            Node currentConditionNode = condition.First;
            while(currentConditionNode != null)
            {
                compiledStatement.AddLast(currentConditionNode);
                currentConditionNode = currentConditionNode.Next;
            }
			compiledStatement.AddLast(NodeFactory.Create("ConditionalJump")); // De body komt dus rechtstreeks na de conditionalJumpNode (dus op de .Next property)
            Node currentBodyNode = body.First;
            while(currentBodyNode != null)
            {
                compiledStatement.AddLast(currentBodyNode);
                currentBodyNode = currentBodyNode.Next;
            }
            compiledStatement.AddLast(NodeFactory.Create("Jump"));
			compiledStatement.AddLast(NodeFactory.Create("DoNothing"));
		}

		public override DoubleLinkedList Compile (ref LinkedListNode<Token> currentToken)
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

