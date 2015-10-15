using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public class ElseStatement : Statement
	{

		public override Statement Copy ()
		{
			return new ElseStatement ();
		}

		public override DoubleLinkedList Compile (ref LinkedListNode<Token> currentToken)
		{
			int elseLevel = currentToken.Value.Level;
			List<TokenExpectation> expected = new List<TokenExpectation> () {
				new TokenExpectation (elseLevel, TokenEnumeration.Else), 
				new TokenExpectation (elseLevel, TokenEnumeration.OpenBracket), 
				new TokenExpectation (elseLevel + 1, TokenEnumeration.Unknown), // Body
				new TokenExpectation (elseLevel, TokenEnumeration.ClosedBracket)
			};

			foreach (var expectation in expected) {
				if (expectation.Level == elseLevel) {
					if (currentToken.Value.Type != expectation.TokenType && currentToken.Value.Type != TokenEnumeration.EOL) {
						throw new Exception (String.Format ("Unexpected end of statement, expected {0} on position: {1}:{2}", expectation.TokenType, currentToken.Value.LineNumber, currentToken.Value.LinePosition));
					} else {
						currentToken = currentToken.Next;
					}
				} else if (expectation.Level >= elseLevel) {
					while (currentToken.Value.Level > elseLevel) {
						Statement conditionStatement = StatementFactory.Create (currentToken.Value.Type);
						if (conditionStatement != null) {
							DoubleLinkedList conditionList = conditionStatement.Compile (ref currentToken);
							Node currentConditionNode = conditionList.First;
							while (currentConditionNode != null) {
								//Toevoegen gevonden statementNodes
								compiledStatement.AddLast (currentConditionNode);
								currentConditionNode = currentConditionNode.Next;
							}
						}
						currentToken = currentToken.Next;
					}
				}
			}
			return compiledStatement;
		}
	}
}