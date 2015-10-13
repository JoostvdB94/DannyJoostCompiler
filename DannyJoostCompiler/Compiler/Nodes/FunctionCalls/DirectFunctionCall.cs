using System;
using System.Collections.Generic;
using DannyJoostCompiler.VirtualMachine;

namespace DannyJoostCompiler
{
	public class DirectFunctionCall:AbstractFunctionCall
	{
        public DirectFunctionCall() { }

        public override void SetupParameters(List<Token> parameters)
        {
            Parameters = parameters;
        }

        public override Node Copy()
        {
            return new DirectFunctionCall();
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

