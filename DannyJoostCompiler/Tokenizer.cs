using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DannyJoostCompiler
{
	public class Tokenizer
	{
		public StreamReader CodeReader{ get; private set;}
		private Dictionary<string,TokenEnumeration> tokens;

		public Tokenizer (StreamReader input)
		{
			this.CodeReader = input;
			tokens = new Dictionary<string, TokenEnumeration>();

			//reserved words
			tokens.Add ("if", TokenEnumeration.If);
			tokens.Add ("else if", TokenEnumeration.ElseIf);
			tokens.Add ("else", TokenEnumeration.Else);
			tokens.Add ("do", TokenEnumeration.Do);
			tokens.Add ("while", TokenEnumeration.While);
            tokens.Add("for", TokenEnumeration.For);
            tokens.Add(" ", TokenEnumeration.WhiteSpace);

            //brackets
            tokens.Add ("(", TokenEnumeration.OpenEllips);
			tokens.Add (")", TokenEnumeration.CloseEllips);
			tokens.Add ("{", TokenEnumeration.OpenBracket);
			tokens.Add ("}", TokenEnumeration.ClosedBracket);
			tokens.Add ("[", TokenEnumeration.OpenSquareBracket);
			tokens.Add ("]", TokenEnumeration.ClosedSquareBracket);

			//Assignment
			tokens.Add ("=", TokenEnumeration.Assignment);
			tokens.Add ("+=", TokenEnumeration.Assignment);

			//Comparisation
			tokens.Add ("==", TokenEnumeration.Equals);
			tokens.Add (">", TokenEnumeration.GreaterThan);
			tokens.Add ("<", TokenEnumeration.LesserThan);
			tokens.Add (">=", TokenEnumeration.GreaterOrEquals);
			tokens.Add ("<=", TokenEnumeration.LesserOrEquals);

			tokens.Add ("int", TokenEnumeration.Integer);
			tokens.Add ("double", TokenEnumeration.Double);
			tokens.Add ("string", TokenEnumeration.String);
			tokens.Add ("char", TokenEnumeration.Char);

		}

		public Stack<BaseToken> tokenize(){
			string code = "";
			int lineNumber = 1;
			while (!CodeReader.EndOfStream) {
				code = CodeReader.ReadLine ();
				//tokenizeLine (code.GetEnumerator (),lineNumber);
				tokenizeLine(code.GetEnumerator(),lineNumber);
				lineNumber ++;
			}
			return null;
		}

		void tokenizeLine (CharEnumerator inputEnummerator, int lineNumber)
		{
			string searchString = "";
			while (inputEnummerator.MoveNext ()) {
                searchString += inputEnummerator.Current;
                if (searchString.Length > 0) {
                    if(tokens.ContainsKey(searchString))
                    {
                        TokenEnumeration foundEnumeration = dictionaryContainsLongestKey((CharEnumerator)inputEnummerator.Clone(), searchString);
                        searchString = "";
                    }
				}
			}
		}

		TokenEnumeration dictionaryContainsLongestKey (CharEnumerator inputEnumerator,string searchstring)
		{
			if (inputEnumerator.MoveNext ()) {
				string extendedSearchString = searchstring + inputEnumerator.Current;
				if (tokens.Keys.Any(k => k.StartsWith(extendedSearchString))) {
					TokenEnumeration foundEnumeration = dictionaryContainsLongestKey (inputEnumerator, extendedSearchString);
					if (foundEnumeration == TokenEnumeration.Unknown) {
						return TokenEnumeration.Unknown;
					} else {
						return foundEnumeration;
					}
				} else
                {
                    TokenEnumeration result;
                    tokens.TryGetValue(searchstring, out result);
                    return result;
                }
			}
			return TokenEnumeration.Unknown;
		}
	}
}

