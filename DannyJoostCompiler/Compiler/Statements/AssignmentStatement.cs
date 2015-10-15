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
            currentToken = currentToken.Next;

            Statement assignmentBody = StatementFactory.Create(currentToken.Value.Type);
            while (assignmentBody != null)
            {
                //case x = 4 + 3
                compiledStatement.AddListLast(assignmentBody.Compile(ref currentToken));
                currentToken = currentToken.Next;
                if (currentToken == null)
                {
                    break;
                }
                assignmentBody = StatementFactory.Create(currentToken.Value.Type);
            }

            compiledStatement.AddLast(NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, variableToAssign, 0) }));
            return compiledStatement;
        }

        public override Statement Copy()
        {
            return new AssignmentStatement();
        }
    }
}