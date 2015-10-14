using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer.Compiler
{
    class TypeCompiler : BaseCompiler
    {
        public override void Compile(LinkedListNode<Token> node, Listitem lastInsertedAction)
        {
            // add var node.next.value to varlist with type node.value
        }
    }
}
