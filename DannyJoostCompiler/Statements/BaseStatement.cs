using System;
using System.Reflection;
using System.Linq;

namespace DannyJoostCompiler.Statements
{
	public abstract class BaseStatement
	{

		public static BaseStatement create(String statement = " "){
			statement = statement.First ().ToString ().ToUpper () + statement.Substring(1);
			var type = Assembly.GetExecutingAssembly ().GetTypes ().Where (t => t.Name.Equals (statement + "Statement") && !t.IsInterface && !t.IsAbstract).FirstOrDefault();
			if (type != null) {
				return (BaseStatement)Activator.CreateInstance (type);
			} else {
				return new EmptyStatement();
			}
		}
			
		public virtual void identify(){
			Console.WriteLine ("I'm a baseclass!");
		}
	}

		
}

