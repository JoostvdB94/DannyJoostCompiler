using DannyJoostCompiler;
using DannyJoostCompiler.Datastructures;
using DannyJoostCompiler.VirtualMachine;
using DannyJoostCompiler.VirtualMachine.Commands;
using System.Collections.Generic;
using System;

namespace DannyJoostCompiler.VirtualMachine
{
    public class UltimateVirtualMachine
    {
        public Variable ReturnValue { get; internal set; }
        public Dictionary<string, Variable> Variables { get; set; }
        public Dictionary<string, BaseCommand> Commands { get; set; }

        public UltimateVirtualMachine()
        {
            Variables = new Dictionary<string, Variable>();
            Commands = new Dictionary<string, BaseCommand>();
            ReturnValue = new Variable() { type = TokenEnumeration.Unknown, Value = 0 };

            AddCommands();
        }

        private void AddCommands()
        {
            Commands.Add("C2R", new ConstantToReturnCommand());
            Commands.Add("R2V", new ReturnToVariableCommand());
            Commands.Add("V2R", new VariableToReturnCommand());
            Commands.Add("Add", new PlusCommand());
            Commands.Add("Minus", new MinusCommand());
            Commands.Add("DivideBy", new DivideByCommand());
            Commands.Add("Multiply", new MultiplyCommand());
            Commands.Add("GreatherThanOrEqualTo", new GreatherThanOrEqualTo());
            Commands.Add("GreatherThan", new GreatherThanCommand());
            Commands.Add("LesserThan", new LesserThanCommand());
            Commands.Add("LesserThanOrEqualTo", new LesserThanOrEqualToCommand());
            Commands.Add("Equals", new EqualsCommand());
            Commands.Add("NotEquals", new NotEqualsCommand());
            Commands.Add("DeclareVariableType", new DeclareVariableTypeCommand());
        }

        public void Run(DoubleLinkedList list)
        {
            var currentNode = list.First;
            NextNodeVisitor visitor = new NextNodeVisitor(this);

            AbstractFunctionCall functionNode = currentNode as AbstractFunctionCall;
            while(currentNode != null)
            {
                if(functionNode != null)
                {
                    Commands[functionNode.Identifier].Execute(this, functionNode.Parameters);
                }
                functionNode = currentNode as AbstractFunctionCall;
                //currentNode.execute()?
                currentNode.Accept(visitor);
                currentNode = visitor.NextNode;
            }
        }
    }
}