using DannyJoostCompiler.VirtualMachine;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public abstract class Node
	{

        public string Identifier { get; set; }
        public Node Next { get; set; }
        public Node Previous { get; set; }

        public Node ()
		{
		}

        public virtual void SetupParameters(List<Token> parameters)
        {

        }

        public abstract void Accept(NodeVisitor visitor);

		public abstract void Execute ();

        public abstract Node Copy();
	}
}

