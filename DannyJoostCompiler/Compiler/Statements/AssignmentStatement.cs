using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
    public class AssignmentStatement : Statement
    {
        public override DoubleLinkedList Compile(ref LinkedListNode<Token> currentToken)
        {
            Statement assignmentBody = StatementFactory.Create(currentToken.Next.Value.Type);
            while(assignmentBody == null)
            {
                currentToken = currentToken.Next;
                assignmentBody = StatementFactory.Create(currentToken.Value.Type);
            }
            DoubleLinkedList bodyList = assignmentBody.Compile(ref currentToken);
            Node currentBodyNode = bodyList.First;
            while(currentBodyNode != null)
            {
                compiledStatement.AddLast(currentBodyNode);
                currentBodyNode = currentBodyNode.Next;

            }
            

            //int x = 2 + 3

            return compiledStatement;
        }
    }
}