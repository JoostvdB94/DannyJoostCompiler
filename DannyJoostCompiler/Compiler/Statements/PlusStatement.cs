using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
    public class PlusStatement : Statement
    {
        public override DoubleLinkedList Compile(ref LinkedListNode<Token> currentToken)
        {
            string leftHandValue = currentToken.Previous.Value.Value;
            string rightHandValue = currentToken.Next.Value.Value;

            if (currentToken.Previous.Value.Type == TokenEnumeration.Number)
            {
                string uniqueVariableName = GetUniqueVariableName();
                compiledStatement.AddLast(NodeFactory.Create("DirectFunctionCall", "C2R", new List<Token>() { currentToken.Previous.Value }));
                compiledStatement.AddLast(NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0,0,TokenEnumeration.Unknown, uniqueVariableName,0) }));
                leftHandValue = uniqueVariableName;
            } else if(currentToken.Previous.Value.Type != TokenEnumeration.Identifier)
            {
                leftHandValue = GetUniqueVariableName();
                compiledStatement.AddLast(NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, leftHandValue, 0) }));
            }

            if (currentToken.Next.Value.Type == TokenEnumeration.Number)
            {
                string uniqueVariableName = GetUniqueVariableName();
                compiledStatement.AddLast(NodeFactory.Create("DirectFunctionCall", "C2R", new List<Token>() { currentToken.Next.Value }));
                compiledStatement.AddLast(NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, uniqueVariableName, 0) }));
                rightHandValue = uniqueVariableName;
            }
            else if (currentToken.Next.Value.Type != TokenEnumeration.Identifier)
            {
                rightHandValue = GetUniqueVariableName();
                compiledStatement.AddLast(NodeFactory.Create("DirectFunctionCall", "R2V", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, rightHandValue, 0) }));
            }
            compiledStatement.AddLast(NodeFactory.Create("FunctionCall", "Add", new List<Token>() { Token.create(0, 0, TokenEnumeration.Unknown, leftHandValue, 0), Token.create(0, 0, TokenEnumeration.Unknown, rightHandValue, 0) }));

            return compiledStatement;
        }
    }
}