using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokenizer.Actions;

namespace Tokenizer.Compiler
{
    class BaseCompiler : ICompiler
    {
        public static Linkedlist ActionList {get;set;}

        public virtual void Compile(LinkedListNode<Token> node, Listitem lastInsertedAction) {}

        private static List<LinkedListNode<Token>> GetUntilPartner(ref LinkedListNode<Token> fromToken)
        {

            List<LinkedListNode<Token>> l = new List<LinkedListNode<Token>>();

            Token toToken = fromToken.Value.Partner;
            fromToken = fromToken.Next;
            while (fromToken.Value != toToken)
            {
                l.Add(fromToken);
                fromToken = fromToken.Next;
            }

            return l;
            //Token toToken = fromToken.Value.Partner;
            //fromToken = fromToken.Next;
            //string code = "";
            //while (fromToken.Value != toToken)
            //{
            //    code += fromToken.Value.Text;

            //    fromToken = fromToken.Next;
            //}

            //return code;
        }

        protected static List<LinkedListNode<Token>> GetExpression(ref LinkedListNode<Token> fromToken)
        {
            return GetUntilPartner(ref fromToken);
        }

        protected static List<LinkedListNode<Token>> GetStatement(ref LinkedListNode<Token> fromToken)
        {
            return GetUntilPartner(ref fromToken);
        }

        protected static List<LinkedListNode<Token>> GetUntilLineEnd(ref LinkedListNode<Token> fromToken)
        {
            List<LinkedListNode<Token>> l = new List<LinkedListNode<Token>>();
            while(!fromToken.Value.Description.Equals(Config.Types.LineEnd))
            {
                l.Add(fromToken);
                fromToken = fromToken.Next;
            }

            return l;
        }

        public static void InitList()
        {
            ActionList = new Linkedlist();
            ActionList.AddLast(new DoNothing());
        }

        public static Linkedlist GetLinkedList()
        {
            return ActionList;
        }
    }
}
