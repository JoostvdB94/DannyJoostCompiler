using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DannyJoostCompiler.Datastructures;

namespace DannyJoostCompiler.Compiler.Statements
{
    public class ConstantStatement : Statement
    {

        public override Statement Copy()
        {
            return new ConstantStatement();
        }

        public override DoubleLinkedList Compile(ref LinkedListNode<Token> currentToken)
        {
            compiledStatement.AddLast(NodeFactory.Create("DirectFunctionCall", "C2R", new List<Token>() { currentToken.Value}));
            return compiledStatement;
        }
    }
}
