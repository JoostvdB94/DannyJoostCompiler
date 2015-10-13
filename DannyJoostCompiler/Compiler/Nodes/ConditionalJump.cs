using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DannyJoostCompiler.VirtualMachine;

namespace DannyJoostCompiler.Compiler.Nodes
{
    public class ConditionalJumpNode : Node
    {
        public override void Accept(NodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override Node Copy()
        {
            return new ConditionalJumpNode();
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
