﻿using DannyJoostCompiler.Datastructures;
using System;
using System.Collections.Generic;

namespace DannyJoostCompiler
{
	public class IfStatement : Statement
	{
		public IfStatement ()
		{
		}

        public override DoubleLinkedList Compile(ref LinkedListNode<Token> currentToken)
        {
            throw new NotImplementedException();
        }
    }
}
