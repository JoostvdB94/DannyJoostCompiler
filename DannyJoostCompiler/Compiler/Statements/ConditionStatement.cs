using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public class ConditionStatement:Statement
	{
		
		public override DoubleLinkedList Compile (ref LinkedListNode<Token> currentToken)
		{
			Token leftHandValue = currentToken.Previous.Value;
			Token rightHandValue = currentToken.Next.Value;
			Token operatorCharacter = currentToken.Value;
			this.compiledStatement.AddLast (NodeFactory.Create("DoNothing"));
			if (leftHandValue.Type == TokenEnumeration.Number) {
				this.compiledStatement.AddLast (NodeFactory.Create("DirectFunctionCall", "C2R", new List<Token>() { leftHandValue }));
				this.compiledStatement.AddLast (NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, GetUniqueVariableName(), 0) }));
			}
			if (rightHandValue.Type == TokenEnumeration.Number) {
				this.compiledStatement.AddLast (NodeFactory.Create("DirectFunctionCall", "C2R", new List<Token>() { rightHandValue }));
				this.compiledStatement.AddLast (NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, GetUniqueVariableName(), 0) }));
			}

			FunctionCall operatorFunction;

			switch (operatorCharacter.Type) {
			case TokenEnumeration.Equals:
				operatorFunction = new FunctionCall ("EQUALS", new List<Token> (){ leftHandValue, rightHandValue });
				break;
			case TokenEnumeration.GreaterThan:
				operatorFunction = new FunctionCall ("GREATERTHAN", new List<Token> (){ leftHandValue, rightHandValue });
				break;
			case TokenEnumeration.GreaterOrEquals:
				operatorFunction = new FunctionCall ("GREATEROREQUALS", new List<Token> (){ leftHandValue, rightHandValue });
				break;
			case TokenEnumeration.LesserThan:
				operatorFunction = new FunctionCall ("LESSERTHAN", new List<Token> (){ leftHandValue, rightHandValue });
				break;
			case TokenEnumeration.LesserOrEquals:
				operatorFunction = new FunctionCall ("LESSEROREQUALS", new List<Token> (){ leftHandValue, rightHandValue });
				break;
			case TokenEnumeration.NotEquals:
				operatorFunction = new FunctionCall ("NOTEQUALS", new List<Token> (){ leftHandValue, rightHandValue });
				break;
			default:
				operatorFunction = null;
				break;
			}
			
			this.compiledStatement.AddLast (operatorFunction);
			this.compiledStatement.AddLast (NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, GetUniqueVariableName(), 0) }));
            //Do Nothing?

            return compiledStatement;
		}

		private void AddNodesForToken(Token token){
			if (token.Type == TokenEnumeration.Number) {
				this.compiledStatement.AddLast (NodeFactory.Create("DirectFunctionCall", "C2R", new List<Token>() { token }));
				this.compiledStatement.AddLast (NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, GetUniqueVariableName(), 0) }));
			}
			if (token.Type == TokenEnumeration.Identifier) {
				
			}
		}
	}
}

