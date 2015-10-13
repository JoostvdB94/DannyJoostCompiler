using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
    public class DivideByStatement : Statement
    {

        public override Statement Copy()
        {
            return new DivideByStatement();
        }

        public override DoubleLinkedList Compile(ref LinkedListNode<Token> currentToken)
        {
            string leftHandValue = GetUniqueVariableName();
            string rightHandValue = GetUniqueVariableName();

            //linkerkant opgeslagen als $xxx
            compiledStatement.AddLast(NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, leftHandValue, 0) }));

            currentToken = currentToken.Next;

            Statement statement = StatementFactory.Create(currentToken.Value.Type);
            if (statement != null)
            {
                var compiledNodes = statement.Compile(ref currentToken);

                var currentCompiledNode = compiledNodes.First;

                while (currentCompiledNode != null)
                {
                    compiledStatement.AddLast(currentCompiledNode);

                    currentCompiledNode = currentCompiledNode.Next;
                }
            }
            //rechterkant opgeslagen als $xxx
            compiledStatement.AddLast(NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, rightHandValue, 0) }));

            compiledStatement.AddLast(NodeFactory.Create("FunctionCall", "DivideBy", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, leftHandValue, 0), Token.create(0, 0, TokenEnumeration.Unknown, rightHandValue, 0) }));


            return compiledStatement;
        }
    }
}