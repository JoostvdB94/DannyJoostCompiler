using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public class IfStatement : Statement
	{
		private DoubleLinkedList condition;

		public IfStatement ()
		{
			condition = new DoubleLinkedList ();
		}

        public override DoubleLinkedList Compile(ref LinkedListNode<Token> currentToken)
        {
			int ifLevel = currentToken.Value.Level;
			List<TokenExpectation> expected = new List<TokenExpectation> () {
				new TokenExpectation (ifLevel, TokenEnumeration.If), 
				new TokenExpectation (ifLevel, TokenEnumeration.OpenEllips),
				new TokenExpectation (ifLevel + 1, TokenEnumeration.Unknown), // Condition
				new TokenExpectation (ifLevel, TokenEnumeration.CloseEllips),
				new TokenExpectation (ifLevel, TokenEnumeration.OpenBracket), 
				new TokenExpectation (ifLevel + 1, TokenEnumeration.Unknown), // Body
				new TokenExpectation (ifLevel, TokenEnumeration.ClosedBracket)
			}; 
			foreach (var expectation in expected) {
				if (expectation.Level == ifLevel){
					if (currentToken.Value.Type != expectation.TokenType){
						throw new Exception(String.Format("Unexpected end of statement, expected {0}", expectation.TokenType));
					}else{
						currentToken = currentToken.Next;
					}
				}else if (expectation.Level >= ifLevel){
					Statement conditionStatement = StatementFactory.Create (currentToken.Value.Type);
					while (conditionStatement == null) {
						currentToken = currentToken.Next;
						conditionStatement = StatementFactory.Create (currentToken.Value.Type);
					}

					DoubleLinkedList conditionResult = conditionStatement.Compile (ref currentToken);
					Node currentConditionNode = conditionResult.First;
					while (currentConditionNode != null) {
						compiledStatement.AddLast (currentConditionNode);
						currentConditionNode = currentConditionNode.Next;
					}

					while(currentToken.Value.Level > ifLevel)
					{
						var bodyPartStatement = StatementFactory.Create (currentToken.Value.Type);
						DoubleLinkedList compiledBodyPart = bodyPartStatement.Compile (ref currentToken);
						Node currentBodyStatementNode = compiledBodyPart.First;
						while (currentBodyStatementNode != null) {
							compiledStatement.AddLast (currentBodyStatementNode);
							currentBodyStatementNode = currentBodyStatementNode.Next;
						}
					}
				}
			}
			return compiledStatement;
		}
	}
}

