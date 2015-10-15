using System;

namespace DannyJoostCompiler
{
	public enum TokenEnumeration
	{
        Unknown,
        Plus,
        Minus,
        Multiply,
        DivideBy,
        Identifier,
		Word,
		Integer,
		While,
		Bool,
		If,
		Else,
		String,
		Float,
		Double,
		Switch,
		Char,
		Number,
		QuotedString,
		WhiteSpace,
		Symbol,
		EOL,
		EOF,
		OpenEllips,
		CloseEllips,
		OpenBracket,
		ClosedBracket,
		OpenSquareBracket,
		ClosedSquareBracket,
		Assignment,
		GreaterThan,
		GreaterOrEquals,
		LesserThan,
		LesserOrEquals,
		Equals,
		Not,
		NotEquals,
		ElseIf,
        FunctionCall,
        QuotedCharacter,
        Separator,
        Comment
    }
}

