using System;
using System.Collections.Generic;
using Tokenizer.Compiler;

namespace Tokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer t = new Tokenizer();

            t.Tokenize("if(12 == 2) { 1+45; ~ \"iets\"6;} else {} while(1==2) { show(); }");
            //t.Tokenize("var x = 3; var y = x + 3;");
            //t.Tokenize("var x = 5; var y = 3 + 6 * x; if ( x < 3 ) { }");

            LinkedListNode<Token> to = t.TokenList.First;
            while (to != null)
            {
                Token token = to.Value;
                System.Console.WriteLine(token.Text + " : " +token.Description + ", at level: "+token.Level);
                to = to.Next;
            }

            t.ValidateTokens();

            Listitem action = BaseCompiler.ActionList.First;

            while (action != null)
            {
                Console.WriteLine(action.GetType());
                action = action.Next;
            }

            VirtualMachine vm = new VirtualMachine();
            vm.action = (Actions.Action) BaseCompiler.ActionList.First;
            vm.runCode();

            System.Console.ReadLine();
            
        }
    }
}
