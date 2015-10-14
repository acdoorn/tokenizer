using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokenizer.Actions;

namespace Tokenizer.Compiler
{
    class IfCompiler : BaseCompiler
    {
        public override void Compile(LinkedListNode<Token> node, Listitem lastInsertedAction)
        {
            node = node.Next;
            if (node.Value.Text == "(")
            {
                DoNothing end = new DoNothing();

                JumpConditional jc = new JumpConditional();
                jc.Condition = GetExpression(ref node);
                jc.OnTrue = new DoNothing();
                ActionList.AddLast(jc);
                ActionList.AddLast(jc.OnTrue);

                node = node.Next;

                if (node.Value.Text == "{")
                {
                    List<LinkedListNode<Token>> statement = GetStatement(ref node);
                    Jump tmpjump = new Jump();
                    tmpjump.Goto = end;
                    tmpjump.Statement = statement;
                    ActionList.AddLast(tmpjump);
                }

                // Hier moet eigelijk nog een else check komen
                jc.OnFalse = end;
                
                
                ActionList.AddLast(end);
            }
            else
                Console.WriteLine("Expected '(' exception");
        }
    }
}
