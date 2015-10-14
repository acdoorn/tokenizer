using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer.Compiler
{
    interface ICompiler
    {   
        void Compile(LinkedListNode<Token> node, Listitem lastInsertedAction);
    }
}
