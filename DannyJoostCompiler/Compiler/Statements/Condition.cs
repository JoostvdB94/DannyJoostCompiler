using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public class Condition:Statement
	{
		private int currentVariable;

		public Condition ()
		{
			currentVariable = 1;
		}

		public override LinkedList<Node> Compile (LinkedListNode<Token> currentToken)
		{
			Token LeftHandValue = currentToken.Previous.Value;
			Token RightHandValue = currentToken.Next.Value;
			Token Operator = currentToken.Value;
			this.compiledStatement.AddLast (NodeFactory.create("DoNothing"));
			if (LeftHandValue.Type == TokenEnumeration.Number) {
				this.compiledStatement.AddLast (NodeFactory.create("DirectFunctionCall", "C2R", new List<string>() { LeftHandValue.Value }));
				this.compiledStatement.AddLast (NodeFactory.create("DirectFunctionCall", "R2V", new List<string>() { "$001" }));
			}
			if (RightHandValue.Type == TokenEnumeration.Number) {
				this.compiledStatement.AddLast (NodeFactory.create("DirectFunctionCall", "C2R", new List<string>() { RightHandValue.Value.ToString() }));
				this.compiledStatement.AddLast (NodeFactory.create("DirectFunctionCall", "R2V", new List<string>() { "$002" }));
			}

			FunctionCall operatorFunction;

			switch (Operator.Type) {
			case TokenEnumeration.Equals:
				operatorFunction = new FunctionCall ("EQUALS", new List<string> (){ LeftHandValue.Value, RightHandValue.Value });
				break;
			case TokenEnumeration.GreaterThan:
				operatorFunction = new FunctionCall ("GREATERTHAN", new List<string> (){ LeftHandValue.Value, RightHandValue.Value });
				break;
			case TokenEnumeration.GreaterOrEquals:
				operatorFunction = new FunctionCall ("GREATEROREQUALS", new List<string> (){ LeftHandValue.Value, RightHandValue.Value });
				break;
			case TokenEnumeration.LesserThan:
				operatorFunction = new FunctionCall ("LESSERTHAN", new List<string> (){ LeftHandValue.Value, RightHandValue.Value });
				break;
			case TokenEnumeration.LesserOrEquals:
				operatorFunction = new FunctionCall ("LESSEROREQUALS", new List<string> (){ LeftHandValue.Value, RightHandValue.Value });
				break;
			case TokenEnumeration.NotEquals:
				operatorFunction = new FunctionCall ("NOTEQUALS", new List<string> (){ LeftHandValue.Value, RightHandValue.Value });
				break;
			default:
				operatorFunction = null;
				break;
			}
			
			this.compiledStatement.AddLast (operatorFunction);
			this.compiledStatement.AddLast (NodeFactory.create("DirectFunctionCall", "R2V", new List<string>() { "$" + currentVariable.ToString("D3") }));
            //Do Nothing?

            return compiledStatement;
		}

		private void AddNodesForToken(Token token){
			if (token.Type == TokenEnumeration.Number) {
				this.compiledStatement.AddLast (NodeFactory.create("DirectFunctionCall", "C2R", new List<string>() { token.Value.ToString() }));
				this.compiledStatement.AddLast (NodeFactory.create("DirectFunctionCall", "R2V", new List<string>() { "$" + currentVariable.ToString("D3") }));
			}
			if (token.Type == TokenEnumeration.Identifier) {
				
			}
		}
	}
}

