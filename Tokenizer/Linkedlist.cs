namespace Tokenizer
{
    class Linkedlist
    {
        public Listitem First { get; set; }
        public Listitem Last { get; set; }

        public void AddLast(Listitem item)
        {
            if (First == null)
            {
                First = item;
            }
            else
            {
                Last.Next = item;
                item.Previous = Last;
            }
            Last = item;
        }

        public void InsertBefore(Listitem before, Listitem insert)
        {
            Listitem tmp = before.Previous;
            before.Previous = insert;
            insert.Previous = tmp;
            insert.Next = before;
        }
    }
}
