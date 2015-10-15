using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler.Compiler
{
    public class NodeCompiler
    {

        public DoubleLinkedList CompileLinkedList(LinkedList<Token> tokens)
        {
            DoubleLinkedList nodeList = new DoubleLinkedList();
            nodeList.AddLast(NodeFactory.Create("DoNothing"));
            LinkedListNode<Token> currentToken = tokens.First;
            while (currentToken != null)
            {
                Statement statement = StatementFactory.Create(currentToken.Value.Type);
                if (statement != null)
                {
                    nodeList.AddListLast(statement.Compile(ref currentToken));
                }
                currentToken = currentToken.Next;
            }
            return nodeList;
        }
    }
}

