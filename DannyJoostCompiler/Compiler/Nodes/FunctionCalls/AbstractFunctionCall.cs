using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public abstract class AbstractFunctionCall:Node
	{
		protected string functionName;
		protected List<string> parameters;

		public AbstractFunctionCall(string functionName){
			this.functionName = functionName;
			this.parameters = new List<string> ();
		}

	}
}

