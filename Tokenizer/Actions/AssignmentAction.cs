using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer.Actions
{
    class AssignmentAction : Action
    {
        public LinkedListNode<Token> LeftValue { get; set; }
        public List<LinkedListNode<Token>> RightValue { get; set; }

        public int Operator(string logic, int x, int y)
        {
            switch (logic)
            {
                case "+": return x + y;
                case "-": return x - y;
                case "/": return x / y;
                case "*": return x * y;
                default: throw new Exception("invalid logic");
            }
        }

        public override Action execute(VirtualMachine vm)
        {
            int? sum = null ;
            int? last = null;
            String lastoperator = null;
            foreach (LinkedListNode<Token> token in RightValue)
            {          
                Token t = token.Value;
                int? current = null;
                switch (t.Description)
                {
                    case Config.Types.MathOperator:
                        lastoperator = t.Text;
                        break;

                    case Config.Types.Variable:
                        current = vm.getVar(t.Text);                                      
                        break;

                    default:
                        current = Convert.ToInt32(t.Text);                                      
                        break;
                }
                if (last.HasValue && !String.IsNullOrEmpty(lastoperator) && current.HasValue)
                    last = Operator(lastoperator, last.Value, current.Value);
                else if (!last.HasValue)
                    last = current;
            }
            if(last.HasValue)
                sum = last.Value;

            vm.vars[LeftValue.Value.Text] = sum;
            return (Action) Next;
        }
    }
}
