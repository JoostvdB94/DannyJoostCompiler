using System;
using System.Reflection;
using System.Linq;

namespace DannyJoostCompiler.Statements
{
	public abstract class BaseStatement
	{
		public static BaseStatement create(String statement = " "){
			var type = Assembly.GetExecutingAssembly ().GetTypes ().Where (t => t.Name.ToLower().Equals (statement.ToLower() + "statement") && !t.IsInterface && !t.IsAbstract).FirstOrDefault();
			if (type != null) {
				return (BaseStatement)Activator.CreateInstance (type);
			} else {
				return new EmptyStatement();
			}
		}
			
		public virtual void identify(){
			Console.WriteLine ("I'm a baseclass!");
		}

		public abstract CharEnumerator SetupParameters (CharEnumerator inputEnummerator);
	}
}

