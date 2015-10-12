using System;

namespace DannyJoostCompiler
{
	public class DoNothingNode:Node
	{
		public DoNothingNode ()
		{
		}

        public override Node Copy()
        {
            return (DoNothingNode)MemberwiseClone();
        }
        
        public override void Execute ()
		{
			
		}
	}
}

