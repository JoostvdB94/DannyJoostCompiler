using System;
using System.Collections.Generic;
using DannyJoostCompiler.VirtualMachine;

namespace DannyJoostCompiler
{
	public class FunctionCall:AbstractFunctionCall
	{

        public FunctionCall() { }

		public FunctionCall (string functionName, List<Token> tokens):base(functionName)
		{
			this.Parameters = tokens;
		}
       
        public override void SetupParameters(List<Token> tokens)
        {
            this.Parameters = tokens; 
        }

        public override Node Copy()
        {
            return new FunctionCall();
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override void Accept(NodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

