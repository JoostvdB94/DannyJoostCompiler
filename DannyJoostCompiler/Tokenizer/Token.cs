using System;

namespace DannyJoostCompiler
{
	public class Token
	{
        private Token() { }

		public int LineNumber { get; set;} 
		public int LinePosition { get; set;}
		public TokenEnumeration Type { get; set;}
		public string Value { get; set;}
		public int Level { get; set;}
		public Token Partner { get; set;}

		public static Token create(int lineNumber, int linePosition, TokenEnumeration tokenEnumeration, string value, int level, Token partner = null){
            var token = new Token();
            token.LineNumber = lineNumber;
            token.LinePosition = linePosition;
            token.Type = tokenEnumeration;
            token.Value = value;
            token.Level = level;
            token.Partner = partner;
            return token;
		}

        public override string ToString()
        {
            string partner = "";
            if(Partner != null)
            {
                partner = Partner.Value;
            }
            return string.Format("{0}:{1}, Type = {2}, Value = {3}, Level = {4}, Partner = {5}", LineNumber, LinePosition,
                Type, Value, Level, partner);
        }
    }
}

