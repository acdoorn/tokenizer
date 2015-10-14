using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer
{
    class VirtualMachine
    {
        public Actions.Action action { get; set; }
        public Dictionary<String, int?> vars { get; set; }

        public void runCode()
        {
            vars = new Dictionary<string,int?>();
            while(action !=null)
            {
                action = action.execute(this);
            }
        }

        public int? getVar(String key)
        {
            if (!vars.ContainsKey(key))
                vars[key] = null;
            return vars[key];
        }
    }
}
