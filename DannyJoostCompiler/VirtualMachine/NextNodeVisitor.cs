using DannyJoostCompiler;
using DannyJoostCompiler.Compiler.Nodes;
using DannyJoostCompiler.VirtualMachine;
using System.Collections.Generic;

public class NextNodeVisitor : NodeVisitor
{
    public Node NextNode { get; private set; }

    public void Visit(DoNothingNode node)
    {
       NextNode = node.Next;
    }
    public void Visit(JumpNode node)
    {
        //NextNode = node.JumpToNode;
    }
    public void Visit(ConditionalJumpNode node) { }
    public void Visit(DirectFunctionCall node) {
        NextNode = node.Next;
    }
    public void Visit(FunctionCall node) {
        NextNode = node.Next;
    }
}