using DannyJoostCompiler.Compiler.Nodes;
using System.Collections.Generic;

namespace DannyJoostCompiler.VirtualMachine
{
    public interface NodeVisitor
    {
        void Visit(DoNothingNode node);
        void Visit(JumpNode node);
        void Visit(ConditionalJumpNode node);
        void Visit(DirectFunctionCall node);
        void Visit(FunctionCall node);
    }
}