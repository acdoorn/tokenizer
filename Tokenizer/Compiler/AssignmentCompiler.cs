using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokenizer.Actions;

namespace Tokenizer.Compiler
{
    class AssignmentCompiler : BaseCompiler
    {
        public override void Compile(LinkedListNode<Token> node, Listitem lastInsertedAction)
        {
            if(node.Previous.Value.Description.Equals(Config.Types.Variable)) {
                LinkedListNode<Token> next = node.Next;

                AssignmentAction aa = new AssignmentAction();
                aa.LeftValue = node.Previous;
                aa.RightValue = GetUntilLineEnd(ref next); // Moet alles van na de = tot een line end (;) gaan

                ActionList.AddLast(aa);
                ActionList.AddLast(new DoNothing());
            } else
                Console.WriteLine("Expected L-value to be a variable");
        }
    }
}
