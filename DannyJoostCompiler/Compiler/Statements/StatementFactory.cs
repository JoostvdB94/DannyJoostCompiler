using System;
using System.Collections.Generic;
using DannyJoostCompiler.DictionaryExtension;

namespace DannyJoostCompiler
{
	public class StatementFactory
	{
		Dictionary<TokenEnumeration,Type> statements;
		public StatementFactory ()
		{
			this.FillStatements (); 
		}

		public static Statement create(Token token){
			Type result = null;
			statements.TryGetValue (token.Type, out result);

			if (result != null) {
				return (Statement)Activator.CreateInstance (result);
			}

			return null;
		}

		void FillStatements ()
		{
			statements.Add (TokenEnumeration.If, null);
			statements.Add (TokenEnumeration.Else, null);
			statements.Add (TokenEnumeration.While, null);
			statements.Add (TokenEnumeration.Assignment, null);

			//operators -> Conditions
			statements.Add (TokenEnumeration.Equals, null);
			statements.Add (TokenEnumeration.GreaterThan, null);
			statements.Add (TokenEnumeration.GreaterOrEquals, null);
			statements.Add (TokenEnumeration.LesserThan, null);
			statements.Add (TokenEnumeration.LesserOrEquals, null);
		}
	}
}

