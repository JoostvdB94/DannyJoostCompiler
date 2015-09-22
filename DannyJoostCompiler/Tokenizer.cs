using System;
using System.Collections.Generic;
using DannyJoostCompiler.Statements;

namespace DannyJoostCompiler
{
	public class Tokenizer
	{
		public string Code{ get; private set;}
		private Dictionary<string,TokenEnumeration> statementTokens;
		private Dictionary<string,TokenEnumeration> bracketTokens;
		private Dictionary<string,TokenEnumeration> comparerTokens;
		private Dictionary<string,TokenEnumeration> typeTokens;
		private Dictionary<string,TokenEnumeration> assignmentTokens;

		public Tokenizer (string input)
		{
			this.Code = input;

			statementTokens = new Dictionary<string, TokenEnumeration>();
			bracketTokens = new Dictionary<string, TokenEnumeration>();
			comparerTokens = new Dictionary<string, TokenEnumeration>();
			typeTokens = new Dictionary<string, TokenEnumeration>();
			assignmentTokens = new Dictionary<string, TokenEnumeration>();

			//reserved words
			statementTokens.Add ("if", TokenEnumeration.If);
			statementTokens.Add ("else if", TokenEnumeration.ElseIf);
			statementTokens.Add ("else", TokenEnumeration.Else);
			statementTokens.Add ("do", TokenEnumeration.Do);
			statementTokens.Add ("while", TokenEnumeration.While);
			statementTokens.Add ("for", TokenEnumeration.For);

			//brackets
			bracketTokens.Add ("(", TokenEnumeration.OpenEllips);
			bracketTokens.Add (")", TokenEnumeration.CloseEllips);
			bracketTokens.Add ("{", TokenEnumeration.OpenBracket);
			bracketTokens.Add ("}", TokenEnumeration.ClosedBracket);
			bracketTokens.Add ("[", TokenEnumeration.OpenSquareBracket);
			bracketTokens.Add ("]", TokenEnumeration.ClosedSquareBracket);

			//Assignment
			assignmentTokens.Add ("=", TokenEnumeration.Assignment);
			assignmentTokens.Add ("+=", TokenEnumeration.Assignment);

			//Comparisation
			comparerTokens.Add ("==", TokenEnumeration.Equals);
			comparerTokens.Add (">", TokenEnumeration.GreaterThan);
			comparerTokens.Add ("<", TokenEnumeration.LesserThan);
			comparerTokens.Add (">=", TokenEnumeration.GreaterOrEquals);
			comparerTokens.Add ("<=", TokenEnumeration.LesserOrEquals);

			typeTokens.Add ("int", TokenEnumeration.Integer);
			typeTokens.Add ("double", TokenEnumeration.Double);
			typeTokens.Add ("string", TokenEnumeration.String);
			typeTokens.Add ("char", TokenEnumeration.Char);
		}

		public void tokenize(CharEnumerator inputEnummerator){
			string searchString = "";
			while (inputEnummerator.MoveNext ()) {
				if (searchString.Length > 0) {
					if (Char.IsWhiteSpace (inputEnummerator.Current) || bracketTokens.ContainsKey (inputEnummerator.Current.ToString())) {
						//is Whitespace, search string!
						if (statementTokens.ContainsKey (searchString)) {
							BaseStatement statement = BaseStatement.create (searchString);
							if (bracketTokens.ContainsKey (inputEnummerator.Current.ToString())) {
								statement.SetupParameters (inputEnummerator);
								statement.identify ();
								searchString = ""; //reset search string
							}
							//Do something with statement
						}

						if (typeTokens.ContainsKey (searchString)) {
							//next characters is the parameter name
							//Pass to another class to extract variable
						}
					}
				} else {
					if (bracketTokens.ContainsKey (inputEnummerator.Current.ToString())) {
						//codeblock
					}
				}
				if (!Char.IsWhiteSpace (inputEnummerator.Current)) {
					//Do not put whitespace in the searchstring!
					searchString += inputEnummerator.Current;
				}

			}
		}
	}
}

