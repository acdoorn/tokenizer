using System;
using System.Collections.Generic;
using Tokenizer.Actions;
using Action = Tokenizer.Actions.Action;

namespace Tokenizer.Compiler
{
    class WhileCompiler : BaseCompiler
    {
        public override void Compile(LinkedListNode<Token> node, Listitem lastInsertedAction)
        {
            node = node.Next;
            if (node.Value.Text == "(")
            {
                List<LinkedListNode<Token>> expression = GetExpression(ref node);
                
                JumpConditional temp = new JumpConditional();
                temp.Condition = expression;
                temp.OnTrue = new DoNothing();
                ActionList.AddLast(temp);
                ActionList.AddLast(temp.OnTrue);
                
                node = node.Next;

                if (node.Value.Text == "{") {
                    List<LinkedListNode<Token>> statement = GetStatement(ref node);
                    Jump tempjump = new Jump();
                    tempjump.Goto = (Action) lastInsertedAction;
                    tempjump.Statement = statement;
                    ActionList.AddLast(tempjump);
                }
                temp.OnFalse = new DoNothing();
                ActionList.AddLast(temp.OnFalse);
            }
            else
                Console.WriteLine("Expected '(' exception");
        }
    }
}
