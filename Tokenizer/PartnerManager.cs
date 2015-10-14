using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer
{
    class PartnerManager
    {
        //public Stack<Token> Parenthsis {get;set;} // ()
        public Stack<Token> Bracket {get;set;} // []
        //public Stack<Token> Brace {get;set;} // {}

        public Stack<Token> IfElse { get; set; }

        public PartnerManager() {
            //Parenthsis = new Stack<Token>();
            Bracket = new Stack<Token>();
            //Brace = new Stack<Token>();

            IfElse = new Stack<Token>();
        }

        //public void Push(char c, Token t)
        //{
        //    switch (c)
        //    {
        //        case '(':
        //        case '{':
        //        case '[':
        //            Bracket.Push(t);
        //            break;
        //    }
        //}

        //public Token Pop(char c)
        //{
        //    switch (c)
        //    {
        //        case ')':
        //        case '}':
        //        case '[':
        //            return Bracket.Pop();
        //    }
        //    return null;
        //}

        public void Reset()
        {
            //Parenthsis.Clear();
            Bracket.Clear();
            //Brace.Clear();
            IfElse.Clear();
        }
    }
}
