using System;

namespace DannyJoostCompiler
{
	public class BaseToken
	{
		public int LineNumber { get; set;} 
		public int LinePosition { get; set;}
		public TokenEnumeration Type { get; set;}
		public string Value { get; set;}
		public int Level { get; set;}
		public int PartnerPosition { get; set;}

		public static BaseToken create(){
			return null;
		}
	}
}

