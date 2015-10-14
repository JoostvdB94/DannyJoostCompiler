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
			Token openEllips = currentToken.Next.Value;
			Token closeEllips = openEllips.Partner;

			List<Token> parameters = new List<Token> ();
			compiledStatement.AddLast (NodeFactory.Create ("DirectFunctionCall", "C2R", new List<Token> (){ Token.create (0, 0, TokenEnumeration.Unknown, "", 0) }));

			//Skip the (
			currentToken = currentToken.Next;

			if (currentToken.Next.Value != closeEllips) {
				//We now know it has parameters
				currentToken = currentToken.Next;

				while (currentToken.Value.Type != TokenEnumeration.Separator && currentToken.Value.Type != closeEllips.Type) {
					Statement firstParamStatement = StatementFactory.Create (currentToken.Value.Type);
					if (firstParamStatement != null) {
						DoubleLinkedList firstParamList = firstParamStatement.Compile (ref currentToken);
						compiledStatement.AddListLast (firstParamList);
					}
					currentToken = currentToken.Next;
				}

				Token oneParam = Token.create (0, 0, TokenEnumeration.Identifier, GetUniqueVariableName (), 0);
				compiledStatement.AddLast (NodeFactory.Create ("DirectFunctionCall", "R2V", new List<Token> (){ oneParam }));
				parameters.Add (oneParam);
		

				if (currentToken.Value.Type == TokenEnumeration.Separator) {
					//Search for more parameters
					//Skip the Separator
					currentToken = currentToken.Next;
					while (currentToken.Value != closeEllips) {
						if (currentToken.Value.Type != TokenEnumeration.Separator) { 
							//We don't want to compile the comma's
							while (currentToken.Value.Type != TokenEnumeration.Separator && currentToken.Value.Type != closeEllips.Type) {
								Statement nextParamStatement = StatementFactory.Create (currentToken.Value.Type);
								if (nextParamStatement != null) {
									DoubleLinkedList nextParamList = nextParamStatement.Compile (ref currentToken);
									compiledStatement.AddListLast (nextParamList);
								}
								currentToken = currentToken.Next;
							}
							Token nextParam = Token.create (0, 0, TokenEnumeration.Identifier, GetUniqueVariableName (), 0);
							compiledStatement.AddLast (NodeFactory.Create ("DirectFunctionCall", "R2V", new List<Token> (){ nextParam }));
							parameters.Add (nextParam);
						} else {
							currentToken = currentToken.Next;	
						}
					}
				}
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

