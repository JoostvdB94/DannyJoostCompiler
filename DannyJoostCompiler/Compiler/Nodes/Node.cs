using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public abstract class Node
	{

        public string Identifier { get; set; }

		public Node ()
		{
		}

        public virtual void SetupParameters(List<string> parameters)
        {

        }

		public abstract void Execute ();

        public abstract Node Copy();
	}
}

