using System;

namespace DannyJoostCompiler.Nodes
{
	public abstract class BaseNode
	{
		private BaseNode next;
		public BaseNode setNext(BaseNode node){
			this.next = node;
			return this;
		}

		public BaseNode getNext(){
			return this.next;
		}

		public virtual void doAction (bool condition = true){
			getNext ().doAction ();
		}
	}
}

