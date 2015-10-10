using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public class FunctionCall:AbstractFunctionCall
	{
		public FunctionCall (string functionName, List<string> parameters):base(functionName)
		{
			this.parameters = parameters;
		}
	}
}

