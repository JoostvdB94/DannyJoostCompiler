using System;
using System.Reflection;
using System.Linq;
using DannyJoostCompiler.Statements;

namespace DannyJoostCompiler.Factories
{
	public class BaseStatementFactory{
		public static BaseStatement create(String statement){
			var type = Assembly.GetExecutingAssembly ().GetTypes ().Where (t => t.FullName.Equals (statement + "Statement")).FirstOrDefault();
			return (BaseStatement)Activator.CreateInstance (type);
		}
	}
		
}

