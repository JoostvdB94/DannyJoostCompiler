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

            RegisterToken(TokenEnumeration.QuotedString, "\\\"(?:[^\\\"\\\\]|\\.)*\\\"");
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
            RegisterToken(TokenEnumeration.Multiply, "\\*");
            RegisterToken(TokenEnumeration.DivideBy, "\\/");
            RegisterToken(TokenEnumeration.Assignment, "=");
            RegisterToken(TokenEnumeration.LesserThan, "<");
            RegisterToken(TokenEnumeration.GreaterThan, ">");
            RegisterToken(TokenEnumeration.LesserOrEquals, "<=");
            RegisterToken(TokenEnumeration.GreaterOrEquals, ">=");
            RegisterToken(TokenEnumeration.Plus, "\\+");
            RegisterToken(TokenEnumeration.Minus, "\\-");
            RegisterToken(TokenEnumeration.Integer, "\\bint\\b");
            RegisterToken(TokenEnumeration.Number, "\\-?\\d+");
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
                            string matchedContent = match.Value;
                            if (pair.Key == TokenEnumeration.Comment)
                            {
                                commentFound = true;
                                break;
                            }
                            if (pair.Key == TokenEnumeration.QuotedString)
                            {
                                matchedContent = matchedContent.Substring(1, matchedContent.Length - 2);
                            }
                            if (pair.Key == TokenEnumeration.Unknown)
                            {
                                PrintError(lineNumber, linePosition, "Illegal character found: " + matchedContent);
                            }
                            Token leftPartner = null;
                            Token token = null;

                            if (pair.Key != TokenEnumeration.WhiteSpace)
                            {
                                int level = ellipsStack.Count + bracketStack.Count;
                                if (pair.Key == TokenEnumeration.ClosedBracket || pair.Key == TokenEnumeration.CloseEllips)
                                {
                                    level--;
                                }
                                token = Token.create(lineNumber, linePosition, pair.Key, matchedContent, level);
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
                                    PrintError(lineNumber + 1, linePosition + 1, "Invalide token '" + matchedContent + "'.");
                                }
                                else
                                {
                                    leftPartner = bracketStack.Pop();
                                    if (leftPartner.Type == TokenEnumeration.OpenBracket)
                                    {
                                        leftPartner.Partner = token;
                                        token.Partner = leftPartner;
                                    }
                                    else
                                    {
                                        PrintError(lineNumber + 1, linePosition + 1, "Invalide token, " + leftPartner.Value + " verwachtte {");
                                    }
                                }
                            }

                            if (pair.Key == TokenEnumeration.CloseEllips)
                            {
                                if (ellipsStack.Count == 0)
                                {
                                    PrintError(lineNumber + 1, linePosition + 1, "Invalide token '" + matchedContent + "'.");
                                }
                                else
                                {
                                    leftPartner = ellipsStack.Pop();
                                    if (leftPartner.Type == TokenEnumeration.OpenEllips)
                                    {
                                        leftPartner.Partner = token;
                                        token.Partner = leftPartner;
                                    }
                                    else
                                    {
                                        PrintError(lineNumber + 1, linePosition + 1, "Invalide token, " + leftPartner.Value + " verwachtte (");
                                    }
                                }
                            }

                            if (pair.Key == TokenEnumeration.Else)
                            {

                                if (statementStack.Count == 0 ||
                                    statementStack.Peek().Type != TokenEnumeration.If)
                                {
                                    PrintError(lineNumber, linePosition, "Invalide token '" + matchedContent + "'.");
                                }
                                else
                                {
                                    leftPartner = statementStack.Pop();
                                    leftPartner.Partner = token;
                                    token.Partner = leftPartner;
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
                tokens.AddLast(Token.create(lineNumber, 0, TokenEnumeration.EOL, "", bracketStack.Count + ellipsStack.Count));
            }
            if (statementStack.Count > 0)
            {
                foreach (var token in statementStack)
                {
                    if (token.Type == TokenEnumeration.If)
                    {
                        continue;
                    }
                    PrintError(token.LineNumber + 1, token.LinePosition + 1, "Partner verwacht voor '" + token.Value + "'.");
                }
            }

            if (ellipsStack.Count > 0)
            {
                foreach (var token in ellipsStack)
                {
                    PrintError(token.LineNumber + 1, token.LinePosition + 1, "Partner verwacht voor '" + token.Value + "'.");
                }
            }

            if (bracketStack.Count > 0)
            {
                foreach (var token in bracketStack)
                {
                    PrintError(token.LineNumber + 1, token.LinePosition + 1, "Partner verwacht voor '" + token.Value + "'.");
                }
            }

            return tokens;
        }

        private void PrintError(int row, int col, string str)
        {
            Console.WriteLine("Error op line " + row + ", column " + col + ". " + str);
        }
    }
}
