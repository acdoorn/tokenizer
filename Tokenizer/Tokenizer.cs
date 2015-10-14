using System;
using System.Collections.Generic;
using Tokenizer.Actions;
using Tokenizer.Compiler;

namespace Tokenizer
{
    class Tokenizer
    {
        private string _validatePart;
        private string _lastValidatePart;
        private bool _lastPartIsValid;

        private bool _isQuoteActive;

        private int _lineNr;
        private int _positionNr;
        private int _level;
        private Config.Types _lastType;

        private Config _config;
        private PartnerManager _pManager;

        public LinkedList<Token> TokenList { get; set; }

        public Tokenizer()
        {
            _config = new Config();
            _pManager = new PartnerManager();

            TokenList = new LinkedList<Token>();
        }

        public void Tokenize(string content)
        {
            _validatePart = "";
            _lastValidatePart = "";

            _lineNr = 0;
            _positionNr = 0;

            _level = 0;

            _isQuoteActive = false;
            

            foreach (string line in content.Split(System.Environment.NewLine.ToCharArray()))
            {
                _lineNr++;
                //Console.WriteLine("lineNr: "+_lineNr);
                foreach (char c in content)
                {
                    _positionNr++;
                    _lastValidatePart = _validatePart;
                    //_validatePart += c;
                    //Console.WriteLine("posNr: " + _positionNr);

                    Console.WriteLine("New char: " + c + ", Current validatePart: "+_validatePart);
                    CheckToken(c);

                    //if(tmp != null)
                    //    this.AddItem(tmp);
                }
                if (_isQuoteActive)
                    Console.WriteLine("No end Quote found");
            }
        }

        public void CheckToken(char lastChar)
        {
            Token t = new Token();

            bool IsValid = false;
            bool isCharValid = false;

            if (_config.IsQuoteChar(lastChar)) {
                HandleQuote(t, lastChar);
                isCharValid = true;
            }
            else {
                double n;
                isCharValid = (_config.IsValidChar(lastChar) || _config.IsStatementOperatorChar(lastChar) || double.TryParse(lastChar.ToString(), out n));
                if (IsNumber(lastChar)) {
                    IsValid = true;
                    _lastType = Config.Types.Number;
                }
                else if (IsValidChar(lastChar)) {
                    IsValid = true;
                }
                else if (IsStatementOperatorChar(lastChar)) {
                    IsValid = true;
                }

                if (!IsValid)
                    if (_lastPartIsValid) {
                        Console.WriteLine("Last part is valid");
                        // Create token
                        SetTokenValues(t);

                        if (t.Text == "if")
                            _pManager.IfElse.Push(t);
                            //Console.WriteLine("Push token");
                        else if (t.Text == "else")
                            _pManager.IfElse.Pop().Partner = t;
                            //Console.WriteLine("Pop token");

                        TokenList.AddLast(t);
                        // Reset values
                        ResetTokenValues();
                        
                        if (isCharValid)
                            CheckToken(lastChar);
                    }

                bool addToken = false;
                Token to = new Token();
                if(_config.IsLineEndChar(lastChar)) {
                    Console.WriteLine("Line end found");

                    //HandleBracketChar(lastChar);
                        
                    _lastType = Config.Types.LineEnd;
                    addToken = true;

                } else if (_config.IsBracketChar(lastChar)) {
                    // Level ophogen
                    _lastType = Config.Types.Bracket;

                    switch(lastChar) {
                        case '(':
                        case '{':
                        case '[':
                            // open bracket
                            _pManager.Bracket.Push(to);
                            _level++;
                            break;
                        case ')':
                        case '}':
                        case ']':
                            // close bracket
                            Token tmp = _pManager.Bracket.Pop();
                            tmp.Partner = to;
                            _level--;
                            break;
                    }
                    addToken = true;
                }
                else if (_config.IsMathOperatorChar(lastChar)) {
                    _lastType = Config.Types.MathOperator;
                    addToken = true;
                }

                if (addToken) {
                    AddToken(lastChar, to);
                    isCharValid = true;
                }
            }

            if (!isCharValid && !_isQuoteActive && !_config.IsWhiteSpaceChar(lastChar)) {
                // Invalid char error
                Console.WriteLine("Invalid character found");

                _lastType = Config.Types.Error;
                AddToken(lastChar, t);
            }
        }

        public void ValidateTokens()
        {
            BaseCompiler.InitList();

            LinkedListNode<Token> node = TokenList.First;
            while (node != null)
            {
                // Check token
                node = ValidateToken(node);
                node = node.Next;
            }
        }

        private LinkedListNode<Token> ValidateToken(LinkedListNode<Token> node)
        {
            Token t = node.Value;
            BaseCompiler tmp = null;
            if (!t.Description.Equals(Config.Types.Word))
                tmp = FactoryCompiler.Create(t.Description.ToString() + "Compiler");
            else
                tmp = FactoryCompiler.Create(UppercaseFirst(t.Text) + "Compiler");
            if (tmp != null)
                tmp.Compile(node, BaseCompiler.GetLinkedList().Last); 

            return node;
        }

        private void AddToken(char c, Token t)
        {
            _lastValidatePart = c.ToString();

            SetTokenValues(t);
            ResetTokenValues();

            TokenList.AddLast(t);
        }

        private bool IsNumber(char c)
        {
            double n;
            if (double.TryParse(c.ToString(), out n))
            {
                if (double.TryParse(_validatePart + c, out n))
                {
                    _validatePart += c;

                    _lastPartIsValid = true;

                    Console.WriteLine("Current number is valid");
                    return true;
                }
            }
            return false;
        }

        private bool IsValidChar(char c)
        {
            if (_config.IsValidChar(c))
            {
                _validatePart += c;

                // Check if new _validatePart is a valid token
                if (_config.IsValidWord(_validatePart))
                {
                    _lastPartIsValid = true;
                    _lastType = Config.Types.Word;
                    Console.WriteLine("Current is valid");
                    Console.WriteLine(_lastValidatePart);

                    return true;
                }
                else if (_config.IsValidFunction(_validatePart))
                {
                    _lastPartIsValid = true;
                    _lastType = Config.Types.Function;
                    return true;
                }
                else if (_config.IsValidVar(_validatePart))
                {
                    _lastPartIsValid = true;
                    _lastType = Config.Types.Variable;
                    return true;
                }
                else if (TokenList.Last != null && _config.IsValidType(TokenList.Last.Value.Text))
                {
                    _lastPartIsValid = true;
                    _lastType = Config.Types.Variable;
                    _config.AddVar(_validatePart);
                    return true;
                }
                else if (_config.IsValidType(_validatePart))
                {
                    _lastPartIsValid = true;
                    _lastType = Config.Types.Type;
                    return true;
                }
            }

            return false;
        }

        private void HandleBracketChar(char c)
        {
            _lastValidatePart = c.ToString();
            _lastType = Config.Types.Bracket;
        }

        private void HandleOperatorChar(char c)
        {
            _lastValidatePart = c.ToString();
            _lastType = Config.Types.MathOperator;
        }

        private bool IsStatementOperatorChar(char c)
        {
            if (_config.IsStatementOperatorChar(c))
            {
                _validatePart += c;
                if (_config.IsStatementOperator(_validatePart))
                {
                    _lastPartIsValid = true;
                    Console.WriteLine("Statement OP is valid");
                    _lastType = Config.Types.StatementOperator;
                    return true;
                }
                else if (_config.IsValidAssignment(_validatePart))
                {
                    _lastPartIsValid = true;
                    _lastType = Config.Types.Assignment;
                    return true;
                }
            }

            return false;
        }

        private void HandleQuote(Token t, char c)
        {
            _validatePart += c;
            _isQuoteActive = !_isQuoteActive;

            if (!_isQuoteActive)
            {
                _lastType = Config.Types.String;
                _lastValidatePart = _validatePart;

                SetTokenValues(t);
                ResetTokenValues();

                TokenList.AddLast(t);
            }
        }

        private void ResetTokenValues()
        {
            // Reset tokenPattern
            _lastValidatePart = "";
            _validatePart = "";
            _lastPartIsValid = false;
        }

        private void SetTokenValues(Token t)
        {
            t.Linenumber = _lineNr;
            t.PositionInLine = _positionNr;
            t.Text = _lastValidatePart;
            t.Level = _level;
            t.Description = _lastType;
        }

        private string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

    }
}
