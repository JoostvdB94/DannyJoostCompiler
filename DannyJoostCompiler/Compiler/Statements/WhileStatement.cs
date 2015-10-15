using DannyJoostCompiler.Compiler.Nodes;
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
		}

		public override Statement Copy ()
		{
			return new WhileStatement ();
		}

		public override DoubleLinkedList Compile (ref LinkedListNode<Token> currentToken)
		{
			Node firstDoNothing = NodeFactory.Create ("DoNothing");
			Node firstDoNothingInsideWhile = NodeFactory.Create ("DoNothing");

			JumpNode insideWhileJumpNode = (JumpNode)NodeFactory.Create ("Jump");
			ConditionalJumpNode conditionalJumpNode = (ConditionalJumpNode)NodeFactory.Create ("ConditionalJump");
			Node lastDoNothing = NodeFactory.Create ("DoNothing");
			insideWhileJumpNode.JumpToNode = firstDoNothing;

			conditionalJumpNode.FalseRoute = lastDoNothing;
			conditionalJumpNode.TrueRoute = firstDoNothingInsideWhile;

			compiledStatement.AddLast (firstDoNothing);

			int whileLevel = currentToken.Value.Level;
			List<TokenExpectation> expected = new List<TokenExpectation> () {
				new TokenExpectation (whileLevel, TokenEnumeration.While), 
				new TokenExpectation (whileLevel, TokenEnumeration.OpenEllips),
				new TokenExpectation (whileLevel + 1, TokenEnumeration.Unknown), // Condition
				new TokenExpectation (whileLevel, TokenEnumeration.CloseEllips),
				new TokenExpectation (whileLevel, TokenEnumeration.OpenBracket), 
				new TokenExpectation (whileLevel + 1, TokenEnumeration.Unknown), // Body
				new TokenExpectation (whileLevel, TokenEnumeration.ClosedBracket)
			};
                        
			foreach (var expectation in expected) {
				if (expectation.Level == whileLevel) {
					if (currentToken.Value.Type != expectation.TokenType && currentToken.Value.Type != TokenEnumeration.EOL) {
						throw new Exception (String.Format ("Unexpected end of statement, expected {0} on position: {1}:{2}", expectation.TokenType, currentToken.Value.LineNumber, currentToken.Value.LinePosition));
					} else {
						currentToken = currentToken.Next;
					}
				} else if (expectation.Level >= whileLevel) {
					if (condition.First == null) {
						//Check if condition is already filled, if not, fill it!
						while (currentToken.Value.Level > whileLevel) {
							Statement conditionStatement = StatementFactory.Create (currentToken.Value.Type);
							if (conditionStatement != null) {
								DoubleLinkedList conditionList = conditionStatement.Compile (ref currentToken);
								Node currentConditionNode = conditionList.First;
								while (currentConditionNode != null) {
									//Toevoegen gevonden statementNodes
									condition.AddLast (currentConditionNode);
									currentConditionNode = currentConditionNode.Next;
								}
							}
							currentToken = currentToken.Next;
						}

						condition.AddLast (conditionalJumpNode);
						compiledStatement.AddListLast (condition);
					} else {
						//Condition is filled, so now we are filling the body!
						body.AddLast (firstDoNothingInsideWhile);
						while (currentToken.Value.Level > whileLevel) {
							Statement conditionStatement = StatementFactory.Create (currentToken.Value.Type);
							if (conditionStatement != null) {
								DoubleLinkedList conditionList = conditionStatement.Compile (ref currentToken);
								Node currentConditionNode = conditionList.First;
								while (currentConditionNode != null) {
									//Toevoegen gevonden statementNodes
									body.AddLast (currentConditionNode);
									currentConditionNode = currentConditionNode.Next;
								}
							}
							currentToken = currentToken.Next;
						}
						body.AddLast (insideWhileJumpNode);
						compiledStatement.AddListLast (body);
					}
				}
			}

			compiledStatement.AddLast (lastDoNothing);


			return compiledStatement;
		}
	}
}

