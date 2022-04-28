using System.Text;

class Tokenizer
{
    private readonly string _code;
    private int _line = 0;
    private int _column = 0;
    private int _cursor = 0;

    private readonly Dictionary<string, TokenType> _reservedKeywords = new()
    {
        ["num"] = TokenType.Num
    };

    public Tokenizer(string code)
    {
        _code = code;
    }

    private void ReadWhiteSpace()
    {
        while (_code[_cursor] == ' ')
        {
            _cursor++;
            _column++;
        }
    }

    private string ReadIdentifier()
    {
        StringBuilder identifier = new();
        while (char.IsLetterOrDigit(_code[_cursor]))
        {
            identifier.Append(_code[_cursor]);
            _cursor++;
            _column++;
        }
        return identifier.ToString();
    }

    private int ReadNumber()
    {
        StringBuilder number = new();
        while (char.IsDigit(_code[_cursor]))
        {
            number.Append(_code[_cursor]);
            _cursor++;
            _column++;
        }
        return int.Parse(number.ToString());
    }

    public List<Token> Parse()
    {
        List<Token> tokens = new();
        for (_cursor = 0; _cursor < _code.Length;)
        {
            char current = _code[_cursor];
            switch (current)
            {
                case '\0':
                    tokens.Add(new Token { Type = TokenType.EndOfFile, Text = "\0", Line = _line, Column = _column });
                    _column++;
                    _cursor++;
                    break;
                case '\r':
                case '\n':
                    _line++;
                    _column = 0;
                    _cursor++;
                    break;
                case ' ':
                    ReadWhiteSpace();
                    break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    int number = ReadNumber();
                    tokens.Add(new Token { Column = _column, Line = _line, Type = TokenType.Integer, Value = number, Text = number.ToString() });
                    break;
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'g':
                case 'h':
                case 'i':
                case 'j':
                case 'k':
                case 'l':
                case 'm':
                case 'n':
                case 'o':
                case 'p':
                case 'q':
                case 'r':
                case 's':
                case 't':
                case 'u':
                case 'v':
                case 'w':
                case 'x':
                case 'y':
                case 'z':
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                case 'G':
                case 'H':
                case 'I':
                case 'J':
                case 'K':
                case 'L':
                case 'M':
                case 'N':
                case 'O':
                case 'P':
                case 'Q':
                case 'R':
                case 'S':
                case 'T':
                case 'U':
                case 'V':
                case 'W':
                case 'X':
                case 'Y':
                case 'Z':
                case '_':
                    string identifier = ReadIdentifier();
                    _reservedKeywords.TryGetValue(identifier, out var tokenType);
                    tokens.Add(new Token { Type = tokenType != TokenType.Invalid ? tokenType : TokenType.Identifier, Text = identifier, Line = _line, Column = _column });
                    break;
                case '+':
                    tokens.Add(new Token { Type = TokenType.Plus, Text = "+", Line = _line, Column = _column, IsOperator = true });
                    _column++;
                    _cursor++;
                    break;
                case '-':
                    tokens.Add(new Token { Type = TokenType.Minus, Text = "-", Line = _line, Column = _column, IsOperator = true });
                    _column++;
                    _cursor++;
                    break;
                case '*':
                    tokens.Add(new Token { Type = TokenType.Asterisk, Text = "*", Line = _line, Column = _column, IsOperator = true });
                    _column++;
                    _cursor++;
                    break;
                case '/':
                    tokens.Add(new Token { Type = TokenType.Slash, Text = "/", Line = _line, Column = _column, IsOperator = true });
                    _column++;
                    _cursor++;
                    break;
                case '^':
                    tokens.Add(new Token { Type = TokenType.Carret, Text = "^", Line = _line, Column = _column, IsOperator = true });
                    _column++;
                    _cursor++;
                    break;
                case '=':
                    tokens.Add(new Token { Type = TokenType.Equals, Text = "=", Line = _line, Column = _column });
                    _column++;
                    _cursor++;
                    break;
                case '(':
                    tokens.Add(new Token { Type = TokenType.OpenBracket, Text = "(", Line = _line, Column = _column });
                    _column++;
                    _cursor++;
                    break;
                case ')':
                    tokens.Add(new Token { Type = TokenType.CloseBracket, Text = ")", Line = _line, Column = _column });
                    _column++;
                    _cursor++;
                    break;
                case ';':
                    tokens.Add(new Token { Type = TokenType.Semicolon, Text = ";", Line = _line, Column = _column });
                    _column++;
                    _cursor++;
                    break;
                default:
                    throw new Exception($"Character '{current}' is not supported");

            }
        }
        tokens.Add(new Token { Type = TokenType.EndOfFile, Text = "\0", Line = _line, Column = _column });
        return tokens;
    }
}
