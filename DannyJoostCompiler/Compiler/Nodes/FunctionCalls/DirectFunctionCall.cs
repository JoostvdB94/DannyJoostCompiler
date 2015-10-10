using System;

namespace DannyJoostCompiler
{
	public class DirectFunctionCall:AbstractFunctionCall
	{
		public DirectFunctionCall (string functionName,string parameter):base(functionName)
		{
			this.parameters.Add (parameter);
		}
	}
}

