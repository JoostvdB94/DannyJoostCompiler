﻿using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public class ConditionStatement:Statement
	{

        public override Statement Copy()
        {
            return new ConditionStatement();
        }

        public override DoubleLinkedList Compile (ref LinkedListNode<Token> currentToken)
		{
			Token leftHandValue = currentToken.Previous.Value;
			Token rightHandValue = currentToken.Next.Value;
			Token operatorCharacter = currentToken.Value;
			compiledStatement.AddLast (NodeFactory.Create("DoNothing"));
            AddNodesForToken(leftHandValue);
            AddNodesForToken(rightHandValue);

			FunctionCall operatorFunction;

			switch (operatorCharacter.Type) {
			case TokenEnumeration.Equals:
				operatorFunction = new FunctionCall ("Equals", new List<Token> (){ leftHandValue, rightHandValue });
				break;
			case TokenEnumeration.GreaterThan:
				operatorFunction = new FunctionCall ("GreatherThan", new List<Token> (){ leftHandValue, rightHandValue });
				break;
			case TokenEnumeration.GreaterOrEquals:
				operatorFunction = new FunctionCall ("GreatherThanOrEqualTo", new List<Token> (){ leftHandValue, rightHandValue });
				break;
			case TokenEnumeration.LesserThan:
				operatorFunction = new FunctionCall ("LesserThan", new List<Token> (){ leftHandValue, rightHandValue });
				break;
			case TokenEnumeration.LesserOrEquals:
				operatorFunction = new FunctionCall ("LesserThanOrEqualTo", new List<Token> (){ leftHandValue, rightHandValue });
				break;
			case TokenEnumeration.NotEquals:
				operatorFunction = new FunctionCall ("NotEquals", new List<Token> (){ leftHandValue, rightHandValue });
				break;
			default:
				operatorFunction = null;
				break;
			}
			
			compiledStatement.AddLast (operatorFunction);
			compiledStatement.AddLast (NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, GetUniqueVariableName(), 0) }));
            //Do Nothing?

            return compiledStatement;
		}

		private void AddNodesForToken(Token token){
			if (token.Type == TokenEnumeration.Number) {
				compiledStatement.AddLast (NodeFactory.Create("DirectFunctionCall", "C2R", new List<Token>() { token }));
				compiledStatement.AddLast (NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, GetUniqueVariableName(), 0) }));
			}
			if (token.Type == TokenEnumeration.Identifier) {
                compiledStatement.AddLast(NodeFactory.Create("DirectFunctionCall", "V2R", new List<Token>() { token }));
            }
		}
	}
}

