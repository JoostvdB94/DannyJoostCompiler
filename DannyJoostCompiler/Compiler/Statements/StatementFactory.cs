using System;
using System.Collections.Generic;
using DannyJoostCompiler.DictionaryExtension;
using DannyJoostCompiler.Compiler.Statements;

namespace DannyJoostCompiler
{
	public static class StatementFactory
	{
        public static Dictionary<TokenEnumeration, Statement> Statements { private get; set; }
		static StatementFactory ()
		{
            Statements = new Dictionary<TokenEnumeration, Statement>();
			FillStatements (); 
		}

		public static Statement Create(TokenEnumeration type){
            return Statements.GetValue(type);
        }

		public static void FillStatements ()
		{
			Statements.Add (TokenEnumeration.If, new IfStatement());
			Statements.Add (TokenEnumeration.Else, new ElseStatement());
			Statements.Add (TokenEnumeration.While, new WhileStatement());
			Statements.Add (TokenEnumeration.Assignment, new AssignmentStatement());

			Statements.Add (TokenEnumeration.Equals, new ConditionStatement());
			Statements.Add (TokenEnumeration.GreaterThan, new ConditionStatement());
			Statements.Add (TokenEnumeration.GreaterOrEquals, new ConditionStatement());
			Statements.Add (TokenEnumeration.LesserThan, new ConditionStatement());
            Statements.Add(TokenEnumeration.LesserOrEquals, new ConditionStatement());

            Statements.Add(TokenEnumeration.Plus, new PlusStatement());
            Statements.Add(TokenEnumeration.Minus, new MinusStatement());
            Statements.Add(TokenEnumeration.Multiply, new MultiplyStatement());
            Statements.Add(TokenEnumeration.DivideBy, new DivideByStatement());

            Statements.Add(TokenEnumeration.Integer, new IdentifierStatement());
            Statements.Add(TokenEnumeration.Bool, new IdentifierStatement());
            Statements.Add(TokenEnumeration.Double, new IdentifierStatement());
            Statements.Add(TokenEnumeration.String, new IdentifierStatement());
        }
	}
}

