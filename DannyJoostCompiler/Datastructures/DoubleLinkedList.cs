using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyJoostCompiler.Datastructures
{
    public class DoubleLinkedList
    {
        public Node First { get; set; }
        public Node Last { get; set; }
        public List<Node> DebugList { get; set; }

        public DoubleLinkedList()
        {
            DebugList = new List<Node>();
        }

        public void AddLast(Node node)
        {
            DebugList.Add(node);
            if (First == null)
            {
                First = node;
            }
            else
            {
                node.Previous = Last;
                Last.Next = node;
            }
            Last = node;
        }
    }
}