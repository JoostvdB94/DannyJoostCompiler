using System;

namespace DannyJoostCompiler.Conditions
{
	public abstract class Condition
	{
		public Object Left{ get; set;}
		public Object Right{ get; set;}
		public abstract bool evalutateCondition ();
	}
}

