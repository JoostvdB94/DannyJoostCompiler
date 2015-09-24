using System;
using DannyJoostCompiler.Conditions;

namespace DannyJoostCompiler.Nodes
{
	public class ConditionNode:BaseNode
	{
		private Condition condition;
		public ConditionNode (Condition condition)
		{
		}

		public override void doAction (bool condition = true)
		{
			getNext ().doAction (this.condition.evalutateCondition());
		}
	}
}

