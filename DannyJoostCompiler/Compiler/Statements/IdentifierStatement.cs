using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyJoostCompiler.Compiler.Statements
{
    class IdentifierStatement : Statement
    {
        public override DoubleLinkedList Compile(ref LinkedListNode<Token> currentToken)
        {
            compiledStatement.AddLast(NodeFactory.Create("FunctionCall", "DeclareVariableType", new List<Token>() { currentToken.Value, currentToken.Next.Value }));

            currentToken = currentToken.Next;
            return compiledStatement;
        }
    }
}
