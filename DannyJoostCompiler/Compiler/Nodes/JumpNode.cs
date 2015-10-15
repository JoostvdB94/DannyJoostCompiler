using DannyJoostCompiler.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyJoostCompiler.Compiler.Nodes
{
    public class JumpNode : Node
    {
        public Node JumpToNode { get; set; }

        public override Node Copy()
        {
            return new JumpNode();
        }

        public override void Accept(NodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
