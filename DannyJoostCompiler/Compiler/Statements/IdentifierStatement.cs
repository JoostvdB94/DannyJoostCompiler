﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DannyJoostCompiler.Datastructures;

namespace DannyJoostCompiler.Compiler.Statements
{
    public class IdentifierStatement : Statement
    {
        public override Statement Copy()
        {
            return new IdentifierStatement();
        }

        public override DoubleLinkedList Compile(ref LinkedListNode<Token> currentToken)
        {
            compiledStatement.AddLast(NodeFactory.Create("DirectFunctionCall", "V2R", new List<Token>() { Token.create(0, 0, currentToken.Value.Type, currentToken.Value.Value, 0) }));
            return compiledStatement;
        }
    }
}
