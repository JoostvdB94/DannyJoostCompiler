using System;
using DannyJoostCompiler.Datastructures;
using System.Collections.Generic;

namespace DannyJoostCompiler.Compiler.Statements
{
	public class FunctionStatement : Statement
	{
		public FunctionStatement ()
		{
		}

		#region implemented abstract members of Statement

		public override DoubleLinkedList Compile (ref System.Collections.Generic.LinkedListNode<Token> currentToken)
		{
			Token functionCall = currentToken.Value;
			LinkedListNode<Token> openEllips = currentToken.Next;
			LinkedListNode<Token> closeEllips = currentToken.Next;
			List<Token> parameters = new List<Token> ();

			currentToken = openEllips;

			//Search for parameters
			while (!currentToken.Equals (closeEllips)) {
				if (currentToken.Value.Type != TokenEnumeration.Separator) { 
					//We don't want the comma's
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
				}
				currentToken = currentToken.Next;
			}

			compiledStatement.AddLast (NodeFactory.Create ("FunctionCall", functionCall.Value, parameters));
			return compiledStatement;
		}

		public override Statement Copy ()
		{
			return new FunctionStatement ();
		}

		#endregion
	}
}

