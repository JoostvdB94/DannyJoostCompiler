using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
    public class AssignmentStatement : Statement
    {
        public override DoubleLinkedList Compile(ref LinkedListNode<Token> currentToken)
        {
			string variableToAssign = currentToken.Previous.Value.Value;

            Statement assignmentBody = StatementFactory.Create(currentToken.Next.Value.Type);
            while(assignmentBody == null)
            {
                currentToken = currentToken.Next;
				if (currentToken.Value.Type == TokenEnumeration.EOL) {
					break;
				}
                assignmentBody = StatementFactory.Create(currentToken.Value.Type);
            }


			if (assignmentBody != null) {
				//case x = 4 + 3
				DoubleLinkedList bodyList = assignmentBody.Compile (ref currentToken);
				Node currentBodyNode = bodyList.First;
				while(currentBodyNode != null)
				{
					compiledStatement.AddLast(currentBodyNode);
					currentBodyNode = currentBodyNode.Next;
				}
			} else {
				//case x = 4
				compiledStatement.AddLast (NodeFactory.Create ("DirectFunctionCall","C2R",new List<Token>{currentToken.Previous.Value}));
			}
     
			NodeFactory.Create ("DirectFunctionCall", "R2V", new List<Token> () { Token.create (0, 0, TokenEnumeration.Unknown, variableToAssign, 0) });
            return compiledStatement;
        }
    }
}