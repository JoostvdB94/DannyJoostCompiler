using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DannyJoostCompiler.DictionaryExtension;
using System.Text.RegularExpressions;

namespace DannyJoostCompiler
{
    public class Tokenizer
    {
        private Dictionary<TokenEnumeration, string> TokenRegex { get; set; }

        public Tokenizer()
        {
            TokenRegex = new Dictionary<TokenEnumeration, string>();

            RegisterToken(TokenEnumeration.QuotedString, "\".*\"");
            RegisterToken(TokenEnumeration.QuotedCharacter, "\'.\'");
            RegisterToken(TokenEnumeration.If, "\\bif\\b");
            RegisterToken(TokenEnumeration.Else, "\\belse\\b");
            RegisterToken(TokenEnumeration.ClosedBracket, "}");
            RegisterToken(TokenEnumeration.Comment, "//");
            RegisterToken(TokenEnumeration.OpenBracket, "\\{");
            RegisterToken(TokenEnumeration.OpenEllips, "\\(");
            RegisterToken(TokenEnumeration.CloseEllips, "\\)");
            RegisterToken(TokenEnumeration.Separator, ",");
            RegisterToken(TokenEnumeration.Equals, "==");
            RegisterToken(TokenEnumeration.Plus, "\\+");
            RegisterToken(TokenEnumeration.Minus, "\\-");
            RegisterToken(TokenEnumeration.Multiply, "\\*");
            RegisterToken(TokenEnumeration.DivideBy, "\\/");
            RegisterToken(TokenEnumeration.Assignment, "=");
            RegisterToken(TokenEnumeration.LesserThan, "<");
            RegisterToken(TokenEnumeration.Integer, "\\bint\\b");
            RegisterToken(TokenEnumeration.Number, "\\d+");
            RegisterToken(TokenEnumeration.String, "\\bstring\\b");
            RegisterToken(TokenEnumeration.While, "\\bwhile\\b");
            RegisterToken(TokenEnumeration.FunctionCall, "\\w+(?=\\s*\\()");
            RegisterToken(TokenEnumeration.WhiteSpace, "\\s+");
            RegisterToken(TokenEnumeration.Identifier, "\\w+");

            RegisterToken(TokenEnumeration.Unknown, "\\W");
        }

        public void RegisterToken(TokenEnumeration type, string pattern)
        {
            TokenRegex.Add(type, pattern);
        }

        public LinkedList<Token> Tokenize(string[] input)
        {
            var tokens = new LinkedList<Token>();
            var bracketStack = new Stack<Token>();
            var statementStack = new Stack<Token>();
            var ellipsStack = new Stack<Token>();
            bool commentFound = false;
            for (int lineNumber = 0; lineNumber < input.Length; lineNumber++)
            {
                commentFound = false;
                var target = input[lineNumber];

                while (target.Length > 0 && !commentFound)
                {
                    foreach (var pair in TokenRegex)
                    {
                        var regex = new Regex("^" + pair.Value);
                        var match = regex.Match(target);
                        int linePosition = input[lineNumber].Length - target.Length;

                        if (match.Success)
                        {
                            if(pair.Key == TokenEnumeration.Comment)
                            {
                                commentFound = true;
                                break;
                            }
                            if(pair.Key == TokenEnumeration.Unknown)
                            {
                                Error(lineNumber, linePosition, "Illegal character found: " + match.Value);
                            }
                            Token leftPartner = null;
                            Token token = null; 
                            
                            if (pair.Key != TokenEnumeration.WhiteSpace)
                            {
                                token = Token.create(lineNumber, linePosition, pair.Key, match.Value, ellipsStack.Count + bracketStack.Count + statementStack.Count);
                                tokens.AddLast(token);
                                if (leftPartner != null)
                                {
                                    leftPartner.Partner = tokens.Last();
                                }
                            }

                            if (pair.Key == TokenEnumeration.ClosedBracket)

                            {
                                if (bracketStack.Count == 0)
                                {
                                    Error(lineNumber + 1, linePosition + 1, "Invalide token '" + match.Value + "'.");
                                }
                                else
                                {
                                    leftPartner = bracketStack.Pop();
                                    leftPartner.Partner = token;
                                    token.Partner = leftPartner;
                                }
                            }

                            if (pair.Key == TokenEnumeration.CloseEllips)

                            {
                                if (ellipsStack.Count == 0)
                                {
                                    Error(lineNumber + 1, linePosition + 1, "Invalide token '" + match.Value + "'.");
                                }
                                else
                                {
                                    leftPartner = ellipsStack.Pop();
                                    leftPartner.Partner = token;
                                    token.Partner = leftPartner;
                                }
                            }

                            if (pair.Key == TokenEnumeration.Else)
                            {

                                if (statementStack.Count == 0 ||
                                    statementStack.Peek().Type != TokenEnumeration.If )
                                {
                                    Error(lineNumber, linePosition, "Invalide token '" + match.Value + "'.");
                                }
                                else
                                {
                                    leftPartner = statementStack.Pop();
                                    leftPartner.Partner = token;

                                }
                            }

                            target = regex.Replace(target, "", 1);



                            if (pair.Key == TokenEnumeration.OpenEllips)
                            {
                                ellipsStack.Push(tokens.Last());
                            }
                            if (pair.Key == TokenEnumeration.OpenBracket)
                            {
                                bracketStack.Push(tokens.Last());
                            }
                            if (pair.Key == TokenEnumeration.If)
                            {
                                statementStack.Push(tokens.Last());
                            }
                            break;
                        }
                    }
                }
            }
            if (statementStack.Count > 0)
            {
                foreach (var token in statementStack)
                {
                    if (token.Type == TokenEnumeration.If)
                    {
                        continue;
                    }
                    Error(token.LineNumber + 1, token.LinePosition + 1, "Partner verwacht voor '" + token.Value + "'.");
                }
            }

            if (ellipsStack.Count > 0)
            {
                foreach (var token in ellipsStack)
                {
                    Error(token.LineNumber + 1, token.LinePosition + 1, "Partner verwacht voor '" + token.Value + "'.");
                }
            }

            if (bracketStack.Count > 0)
            {
                foreach (var token in bracketStack)
                {
                    Error(token.LineNumber + 1, token.LinePosition + 1, "Partner verwacht voor '" + token.Value + "'.");
                }
            }

            return tokens;
        }

        private void Error(int row, int col, string str)
        {
            Console.WriteLine("Error op line " + row + ", column " + col + ". " + str);
        }
    }
}
