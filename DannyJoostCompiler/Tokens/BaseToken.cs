using System;
using System.Reflection;
using System.Linq;

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

		public static BaseToken create(TokenEnumeration token){
			System.Type type = Assembly.GetExecutingAssembly ().GetTypes ().Where (t => t.Name.ToLower().Equals ((token + "Token").ToLower()) && !t.IsInterface && !t.IsAbstract).FirstOrDefault();
			if (type.Equals (null)) {
				return new BaseToken();
			}
			return (BaseToken)Activator.CreateInstance (type);
		}

	}
}

