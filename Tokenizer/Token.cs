using System;

namespace Tokenizer
{
    class Token
    {
        public int Linenumber {get;set;}
        public int PositionInLine { get; set; }
        public String Text { get; set; }
        public Config.Types Description { get; set; }
        public int Level { get; set; }
        public Token Partner { get; set; }

        public Token()
        {

        }

        public Token(int linenumber, int positioninline, String text, Config.Types description, int level, Token partner)
        {
            Linenumber = linenumber;
            PositionInLine = positioninline;
            Text = text;
            Description = description;
            Level = level;
            Partner = partner;
        }
    }
}
