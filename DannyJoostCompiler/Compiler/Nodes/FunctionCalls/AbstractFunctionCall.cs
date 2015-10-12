using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public abstract class AbstractFunctionCall:Node
	{
		public List<Token> Parameters {  get; set; }

        public AbstractFunctionCall() {
            Parameters = new List<Token>();
        }

        public AbstractFunctionCall(string identifier) : this(){
			Identifier = identifier;
		}

	}
}

