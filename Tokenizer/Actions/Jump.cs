using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer.Actions
{
    class Jump : Action
    {
        public Action Goto { get; set; }
        public List<LinkedListNode<Token>> Statement { get; set; }

        public override Action execute(VirtualMachine vm)
        {
            //moet wel de statement uitvoeren maar deze is niet gevuld.
            return Goto;
        }
    }
       
}
