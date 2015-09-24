using System;

namespace DannyJoostCompiler.Nodes
{
	public class ConditionalJumpNode:BaseNode
	{
		public BaseNode trueRoute { set; private get;}
		public BaseNode falseRoute { set; private get;}

		public ConditionalJumpNode ()
		{
		}

		public override void doAction (bool conditionResult)
		{
			if (conditionResult) {
				trueRoute.doAction ();
			} else {
				falseRoute.doAction ();
			}
		}

	}
}

