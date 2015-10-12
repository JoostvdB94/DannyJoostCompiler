using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public class FunctionCall:AbstractFunctionCall
	{

        public FunctionCall() { }

		public FunctionCall (string functionName, List<string> parameters):base(functionName)
		{
			this.Parameters = parameters;
		}
       
        public override void SetupParameters(List<string> parameters)
        {
            this.Parameters = parameters; 
        }

        public override Node Copy()
        {
            return (FunctionCall)MemberwiseClone();
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}

