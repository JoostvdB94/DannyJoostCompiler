using System;
using DannyJoostCompiler.VirtualMachine;

namespace DannyJoostCompiler
{
	public class DoNothingNode:Node
	{
		public DoNothingNode ()
		{
		}

        public override void Accept(NodeVisitor visitor)
        {
            visitor.Visit(this);
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

