using System;

namespace DannyJoostCompiler
{
	public struct TokenExpectation
	{
		public int Level;
		public TokenEnumeration TokenType;

		public TokenExpectation(int level, TokenEnumeration tokenType)
		{
			Level = level;
			TokenType = tokenType;
		}
	}
}

