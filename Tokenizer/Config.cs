using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tokenizer
{
    class Config
    {
        public enum Types { MathOperator, StatementOperator, Bracket, Number, Word, Variable, String, LineEnd, Error, Function, Assignment, Type };

        public Regex ValidCharsReg { get; set; }
        public Regex StatementOpReg { get; set; }
        public Regex AssignmentOpReg { get; set; }

        public char[] Brackets { get; set; }
        public char[] MathOperators { get; set; }
        public char[] Quotes { get; set; }
        public char[] LineEnd { get; set; }
        public char[] WhiteSpaces { get; set; }
        public string[] Assignments { get; set; }
        public string[] VarTypes { get; set; }

        public string[] StatementOperators { get; set; }
        public string[] Words { get; set; }
        public string[] Functions { get; set; }
        public List<String> Vars { get; set; }

        public Config()
        {
            ValidCharsReg = new Regex(@"^[a-zA-Z0-9\\_]+$");
            StatementOpReg = new Regex(@"^[=|>|<|&|\||!|+|-]+$");

            Brackets = new char[] { '(', ')', '{', '}','[',']'};
            MathOperators = new char[] {'+','-','/','*','%'};
            Quotes = new char[] { '"' };
            LineEnd = new char[] { ';' };
            WhiteSpaces = new char[] { ' ', '\t', '\n' };
            Assignments = new string[] { "=" };
            VarTypes = new string[] { "var" };

            StatementOperators = new string[] { "==", ">=", "<=", ">", "<", "&&", "||", "!", "!=" };
            Words = new string[] { "if", "else", "while" };
            Functions = new string[] { "show" };
            Vars = new List<string>();
        }

        public bool IsValidVar(String w)
        {
            return Vars.IndexOf(w) > -1;
        }

        public void AddVar(String w)
        {
            Vars.Add(w);
        }

        public bool IsValidType(String w)
        {
            return Array.IndexOf(VarTypes, w) > -1;
        }

        public bool IsValidAssignment(String w)
        {
            return Array.IndexOf(Assignments, w) > -1;
        }

        public bool IsValidFunction(String w)
        {
            return Array.IndexOf(Functions, w) > -1;
        }

        public bool IsValidChar(char c)
        {
            return ValidCharsReg.IsMatch(c.ToString());
        }

        public bool IsBracketChar(char c)
        {
            return Array.IndexOf(Brackets, c) > -1;
        }

        public bool IsMathOperatorChar(char c)
        {
            return Array.IndexOf(MathOperators, c) > -1;
        }

        public bool IsQuoteChar(char c)
        {
            return Array.IndexOf(Quotes, c) > -1;
        }

        public bool IsLineEndChar(char c)
        {
            return Array.IndexOf(LineEnd, c) > -1;
        }

        public bool IsWhiteSpaceChar(char c)
        {
            return Array.IndexOf(WhiteSpaces, c) > -1;
        }

        public bool IsStatementOperatorChar(char c)
        {
            return StatementOpReg.IsMatch(c.ToString());
        }

        public bool IsStatementOperator(string o)
        {
            return Array.IndexOf(StatementOperators, o) > -1;
        }

        public bool IsValidWord(string w)
        {
            return Array.IndexOf(Words, w) > -1;
        }

    }
}
