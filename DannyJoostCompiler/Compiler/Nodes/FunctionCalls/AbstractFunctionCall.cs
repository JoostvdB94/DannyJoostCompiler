using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public abstract class AbstractFunctionCall:Node
	{
		public List<string> Parameters { protected get; set; }

        public AbstractFunctionCall() { }

		public AbstractFunctionCall(string identifier){
			Identifier = identifier;
			Parameters = new List<string> ();
		}

	}
}

