using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public class DirectFunctionCall:AbstractFunctionCall
	{
        public DirectFunctionCall() { }

        public override void SetupParameters(List<string> parameters)
        {
            Parameters = parameters;
        }

        public override Node Copy()
        {
            return (DirectFunctionCall)MemberwiseClone();
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}

