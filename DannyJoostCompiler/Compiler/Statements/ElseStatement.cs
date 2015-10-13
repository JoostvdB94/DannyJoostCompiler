using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
    public class ElseStatement : Statement
    {

        public override Statement Copy()
        {
            return new ElseStatement();
        }
        public override DoubleLinkedList Compile(ref LinkedListNode<Token> currentToken)
        {
            throw new NotImplementedException();
        }
    }
}