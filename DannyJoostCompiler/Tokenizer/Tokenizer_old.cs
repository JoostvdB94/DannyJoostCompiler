using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DannyJoostCompiler.DictionaryExtension;

namespace DannyJoostCompiler
{
    public class Tokenizer_old
    {
        public StreamReader CodeReader { get; private set; }
        public Dictionary<string, TokenEnumeration> Tokens { private get; set; }
        public LinkedList<Token> TokenList { private get; set; }
        public Stack<Token> TokenStack { private get; set; }




        public Tokenizer_old(StreamReader input)
        {
            CodeReader = input;
            Tokens = new Dictionary<string, TokenEnumeration>();
            TokenList = new LinkedList<Token>();
            TokenStack = new Stack<Token>();

            InitialiseTokenDictionary();
        }

        private void InitialiseTokenDictionary()
        {
            //reserved words
            Tokens.Add("if", TokenEnumeration.If);
            Tokens.Add("else if", TokenEnumeration.ElseIf);
            Tokens.Add("else", TokenEnumeration.Else);
            Tokens.Add("do", TokenEnumeration.Do);
            Tokens.Add("while", TokenEnumeration.While);
            Tokens.Add("for", TokenEnumeration.For);
            Tokens.Add(" ", TokenEnumeration.WhiteSpace);

            //brackets
            Tokens.Add("(", TokenEnumeration.OpenEllips);
            Tokens.Add(")", TokenEnumeration.CloseEllips);
            Tokens.Add("{", TokenEnumeration.OpenBracket);
            Tokens.Add("}", TokenEnumeration.ClosedBracket);
            Tokens.Add("[", TokenEnumeration.OpenSquareBracket);
            Tokens.Add("]", TokenEnumeration.ClosedSquareBracket);

            //Assignment
            Tokens.Add("=", TokenEnumeration.Assignment);
            Tokens.Add("+=", TokenEnumeration.Assignment);

            //Comparisation
            Tokens.Add("==", TokenEnumeration.Equals);
            Tokens.Add(">", TokenEnumeration.GreaterThan);
            Tokens.Add("<", TokenEnumeration.LesserThan);
            Tokens.Add(">=", TokenEnumeration.GreaterOrEquals);
            Tokens.Add("<=", TokenEnumeration.LesserOrEquals);

            Tokens.Add("int", TokenEnumeration.Integer);
            Tokens.Add("double", TokenEnumeration.Double);
            Tokens.Add("string", TokenEnumeration.String);
            Tokens.Add("char", TokenEnumeration.Char);
        }

        public LinkedList<Token> Tokenize()
        {
            int lineNumber = 1;

            while (!CodeReader.EndOfStream)
            {
                TokenizeLine(CodeReader.ReadLine().ToCharArray(), lineNumber);
                lineNumber++;
            }
            TokenList.AddLast(Token.create(lineNumber, 0, TokenEnumeration.EOF, "", 0, null));
            return TokenList;
        }

        public void TokenizeLine(char[] input, int lineNumber)
        {
            string searchString = "";
            int lineIndex = 0;
            while (lineIndex < input.Length)
            {

                searchString += input[lineIndex];
                if (Tokens.ContainsKey(searchString))
                {

                    string extendedSearchString = searchString;
                    while (input.Length > lineIndex + 1 && Tokens.Any(t => t.Key.StartsWith(extendedSearchString)))
                    {
                        searchString = extendedSearchString;
                        lineIndex++;
                        extendedSearchString += input[lineIndex];
                    }
                    if (!searchString.Equals(" "))
                    {
                        searchString = searchString.Trim();
                    }
                    if (!extendedSearchString.Equals(searchString))
                    {
                        lineIndex--;
                    }

                    Token token = Token.create(lineNumber, lineIndex, Tokens.GetValue(searchString), searchString, 0);

                    if (token.Type == TokenEnumeration.OpenBracket || token.Type == TokenEnumeration.OpenSquareBracket || token.Type == TokenEnumeration.OpenEllips || token.Type == TokenEnumeration.If || token.Type == TokenEnumeration.ElseIf || token.Type == TokenEnumeration.Else || token.Type == TokenEnumeration.ClosedBracket || token.Type == TokenEnumeration.ClosedSquareBracket || token.Type == TokenEnumeration.CloseEllips)
                    {
                        TokenStack.Push(token);
                    }
                    TokenList.AddLast(token);
                    searchString = "";
                }
                lineIndex++;
            }
            TokenList.AddLast(Token.create(lineNumber, lineIndex, TokenEnumeration.EOL, "", 0, null));
        }

        private void FindSyntaxError(Token token)
        {
            if (TokenStack.Count > 0)
            {
                switch (token.Type)
                {
                    case TokenEnumeration.CloseEllips:
                        FindParentMismatchError(token, TokenEnumeration.OpenEllips);
                        break;
                    case TokenEnumeration.ClosedSquareBracket:
                        FindParentMismatchError(token, TokenEnumeration.OpenSquareBracket);
                        break;
                    case TokenEnumeration.ClosedBracket:
                        FindParentMismatchError(token, TokenEnumeration.OpenBracket);
                        break;
                    case TokenEnumeration.Else:

                        foreach (Token currentToken in TokenStack)
                        {
                        }
                        break;
                    case TokenEnumeration.ElseIf:

                        break;
                }
            }
        }

        private void FindParentMismatchError(Token token, TokenEnumeration expectedType)
        {
            if (TokenStack.Peek().Type == expectedType)
            {
                token.Partner = TokenStack.Pop();
                token.Partner.Partner = token;
                return;
            }
            PrintError(token);
        }

        private void FindParentMismatchError(Token token, List<TokenEnumeration> expectedTypes)
        {
            if (expectedTypes.Contains(TokenStack.Peek().Type))
            {
                token.Partner = TokenStack.Pop();
                token.Partner.Partner = token;
                return;
            }
            PrintError(token);
        }

        private void PrintError(Token token)
        {
            Console.WriteLine("Syntax error on" + token.LineNumber + ":" + token.LinePosition + " on token " + token.Value);
        }
    }
}

