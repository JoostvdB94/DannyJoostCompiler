using System;

namespace DannyJoostCompiler
{
	public enum TokenEnumeration
	{
		Unknown,
		Identifier,
		Word,
		Integer,
		While,
		Bool,
		For,
		Do,
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
		BinaryOp,
		UnaryOp,
		MetaValueOp,
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
		ElseIf
	}
}

