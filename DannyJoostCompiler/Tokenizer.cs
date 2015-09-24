using System;
using System.Collections.Generic;
using C5;
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
			tokens.Add ("for", TokenEnumeration.For);

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
			while (CodeReader.EndOfStream) {
				code = CodeReader.ReadLine ();
				//tokenizeLine (code.GetEnumerator (),lineNumber);
				tokenizeLine("if(){}else if(){}else{}".GetEnumerator(),lineNumber);
				lineNumber ++;
			}
			string searchString = "";
			return null;
		}

		void tokenizeLine (CharEnumerator inputEnummerator, int lineNumber)
		{
			string searchString = "";
			while (inputEnummerator.MoveNext ()) {
				if (searchString.Length > 0) {
					TokenEnumeration foundEnumeration =  dictionaryContainsLongestKey ((CharEnumerator)inputEnummerator.Clone (), searchString);
					if (foundEnumeration != TokenEnumeration.Unknown) {
						Console.WriteLine ("We found" + foundEnumeration.ToString ());
					}
				}
				searchString += inputEnummerator.Current;
			}
		}

		TokenEnumeration dictionaryContainsLongestKey (CharEnumerator inputEnumerator,string searchstring)
		{
			if (inputEnumerator.MoveNext ()) {
				string extendedSearchString = searchstring + inputEnumerator.Current;
				if (tokens.Keys.Where(k => k.StartsWith(extendedSearchString)).FirstOrDefault() != null) {
					TokenEnumeration foundEnumeration = dictionaryContainsLongestKey (inputEnumerator, extendedSearchString);
					if (foundEnumeration == TokenEnumeration.Unknown) {
						return TokenEnumeration.Unknown;
					} else {
						return foundEnumeration;
					}
				}
			}
			return TokenEnumeration.Unknown;
		}
	}
}

