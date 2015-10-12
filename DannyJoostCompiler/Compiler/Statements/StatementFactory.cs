using System;
using System.Collections.Generic;
using DannyJoostCompiler.DictionaryExtension;

namespace DannyJoostCompiler
{
	public static class StatementFactory
	{
		public static Dictionary<TokenEnumeration, Statement> statements { private get; set; }
		static StatementFactory ()
		{
			FillStatements (); 
		}

		public static Statement create(TokenEnumeration type){
			Statement statement = statements.GetValue(type);
            if (statement != null)
            {
                return statement;
            }
            throw new KeyNotFoundException("No class found with the name " + type);
        }

		public static void FillStatements ()
		{
			statements.Add (TokenEnumeration.If, null);
			statements.Add (TokenEnumeration.Else, null);
			statements.Add (TokenEnumeration.While, null);
			statements.Add (TokenEnumeration.Assignment, null);

			statements.Add (TokenEnumeration.Equals, null);
			statements.Add (TokenEnumeration.GreaterThan, null);
			statements.Add (TokenEnumeration.GreaterOrEquals, null);
			statements.Add (TokenEnumeration.LesserThan, null);
			statements.Add (TokenEnumeration.LesserOrEquals, null);
		}
	}
}

