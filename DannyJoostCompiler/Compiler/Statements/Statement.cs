using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
    public abstract class Statement
    {
        protected DoubleLinkedList compiledStatement;
        private static long _variableCounter = 0;

        public Statement()
        {
            compiledStatement = new DoubleLinkedList();
        }

        public abstract DoubleLinkedList Compile(ref LinkedListNode<Token> currentToken);

        public abstract Statement Copy();

        public string GetUniqueVariableName()
        {
            _variableCounter++;
            return "$" + _variableCounter.ToString("D6");
        }
    }
}

