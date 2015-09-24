using System;

namespace DannyJoostCompiler.Conditions
{
	public class EqualsCondition:Condition
	{
		public EqualsCondition ()
		{
		}


		#region implemented abstract members of Condition
		public override bool evalutateCondition ()
		{
			return this.Left.Equals (Right);
		}
		#endregion
	}
}

