using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;
using DannyJoostCompiler.Compiler.Nodes;

namespace DannyJoostCompiler
{
	public class IfStatement : Statement
	{
		
		private DoubleLinkedList condition;
		private DoubleLinkedList body;

		public IfStatement ()
		{
			condition = new DoubleLinkedList ();
			body = new DoubleLinkedList ();
		}

		public override Statement Copy ()
		{
			return new IfStatement ();
		}

		public override DoubleLinkedList Compile (ref LinkedListNode<Token> currentToken)
		{
			Node firstDoNothing = NodeFactory.Create ("DoNothing");
			Node firstDoNothingInsideIf = NodeFactory.Create ("DoNothing");

			JumpNode insideWhileJumpNode = (JumpNode)NodeFactory.Create ("Jump");
			ConditionalJumpNode conditionalJumpNode = (ConditionalJumpNode)NodeFactory.Create ("ConditionalJump");
			Node lastDoNothing = NodeFactory.Create ("DoNothing");
			insideWhileJumpNode.JumpToNode = lastDoNothing;

			conditionalJumpNode.FalseRoute = lastDoNothing;
			conditionalJumpNode.TrueRoute = firstDoNothingInsideIf;

			compiledStatement.AddLast (firstDoNothing);

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
				if (expectation.Level == ifLevel) {
					if (currentToken.Value.Type != expectation.TokenType && currentToken.Value.Type != TokenEnumeration.EOL) {
						throw new Exception (String.Format ("Unexpected end of statement, expected {0}", expectation.TokenType));
					} else {
						currentToken = currentToken.Next;
					}
				} else if (expectation.Level >= ifLevel) {
					if (condition.First == null) {
						//Check if condition is already filled, if not, fill it!
						while (currentToken.Value.Level > ifLevel) {
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
						body.AddLast (firstDoNothingInsideIf);
						while (currentToken.Value.Level > ifLevel) {
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

