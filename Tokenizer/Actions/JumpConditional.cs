using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer.Actions
{
    class JumpConditional : Action
    {
        public Action OnTrue { get; set; }
        public Action OnFalse { get; set; }
        public List<LinkedListNode<Token>> Condition { get; set; }
        //public String Condition { get; set; }

        public Boolean Operator(string logic, int x, int y)
        {
            switch (logic)
            {
                case ">": return x > y;
                case "<": return x < y;
                case ">=": return x >= y;
                case "<=": return x <= y;
                case "==": return x == y;
                case "!=": return x != y;
                default: throw new Exception("invalid logic");
            }
        }

        //public Boolean Operator(string logic, bool x, bool y)
        //{
        //    switch (logic)
        //    {
        //        case "&&": return x && y;
        //        case "||": return x || y;
        //        default: throw new Exception("invalid logic");
        //    }
        //}

        public override Action execute(VirtualMachine vm)
        {
            int? last = null;
            bool? lastbool = null;
            String lastoperator = null;
            foreach (LinkedListNode<Token> token in Condition)
            {
                Token t = token.Value;
                int? current = null;
                switch (t.Description)
                {
                    case Config.Types.StatementOperator:
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
                    lastbool = Operator(lastoperator, last.Value, current.Value);
                else if (!last.HasValue)
                    last = current;
            }

            return (lastbool.Value) ? OnTrue : OnFalse;
        }
    }
}
