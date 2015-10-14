using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer.Actions
{
    class Action : Listitem
    {
        public virtual Action execute(VirtualMachine vm) { return null; }
    }
}
