﻿using DannyJoostCompiler.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyJoostCompiler.Compiler.Nodes
{
    public class JumpNode : Node
    {
        public override Node Copy()
        {
            return (JumpNode)MemberwiseClone();
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override void Accept(NodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}