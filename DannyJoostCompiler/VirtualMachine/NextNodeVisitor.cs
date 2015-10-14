using DannyJoostCompiler;
using DannyJoostCompiler.Compiler.Nodes;
using DannyJoostCompiler.VirtualMachine;
using System.Collections.Generic;

public class NextNodeVisitor : NodeVisitor
{
	public Node NextNode { get; private set; }

	public UltimateVirtualMachine VM { get; set; }

	public NextNodeVisitor (UltimateVirtualMachine vm)
	{
		VM = vm;
	}

	public void Visit (DoNothingNode node)
	{
		NextNode = node.Next;
	}

	public void Visit (JumpNode node)
	{
		NextNode = node.JumpToNode;
	}

	public void Visit (ConditionalJumpNode node)
	{
		if (VM.ReturnValue.Value.Equals (true)) {
			NextNode = node.TrueRoute;
		} else {
			NextNode = node.FalseRoute;
		}
	}

	public void Visit (DirectFunctionCall node)
	{
		NextNode = node.Next;
	}

	public void Visit (FunctionCall node)
	{
		NextNode = node.Next;
	}
}