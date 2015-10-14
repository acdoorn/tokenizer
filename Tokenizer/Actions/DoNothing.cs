using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer.Actions
{
    class DoNothing : Action
    {
        public override Action execute(VirtualMachine vm)
        {
            return (Action) Next;
        }
    }
}
