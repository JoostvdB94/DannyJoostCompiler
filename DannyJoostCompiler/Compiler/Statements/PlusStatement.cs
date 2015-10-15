using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
    public class PlusStatement : Statement
    {

        public override Statement Copy()
        {
            return new PlusStatement();
        }

        public override DoubleLinkedList Compile(ref LinkedListNode<Token> currentToken)
        {
            string leftHandValue = GetUniqueVariableName();
            string rightHandValue = GetUniqueVariableName();

            compiledStatement.AddLast(NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, leftHandValue, 0) }));
            currentToken = currentToken.Next;

            Statement statement = StatementFactory.Create(currentToken.Value.Type);
            if (statement != null)
            {
                compiledStatement.AddListLast(statement.Compile(ref currentToken));
            }
            compiledStatement.AddLast(NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, rightHandValue, 0) }));

            compiledStatement.AddLast(NodeFactory.Create("FunctionCall", "Add", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, leftHandValue, 0), Token.create(0, 0, TokenEnumeration.Unknown, rightHandValue, 0) }));

            return compiledStatement;
        }
    }
}