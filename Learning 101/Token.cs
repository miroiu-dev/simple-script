using System.Text.RegularExpressions;

class Token
{
    public int Length => Value.Length;
    public TokenType Type { get; set; }
    public string Value { get; set; }
    public bool IsOperator { get; private set; }
    public int Line { get; set; }
    public int Column { get; set; }

    private static readonly Regex _identifierRegex = new(@"^[aA-zZ]+\d*$");
    public static Token Create(string word)
    {
        TokenType type = TokenType.Invalid;
        bool isOperator = false;

        switch (word)
        {
            case "num":
                type = TokenType.Num;
                break;
            case "=":
                type = TokenType.Equals;
                break;
            case "\0":
                type = TokenType.EndOfFile;
                break;

            case "+":
                type = TokenType.Plus;
                isOperator = true;
                break;
            case "-":
                type = TokenType.Minus;
                isOperator = true;
                break;
            case ";":
                type = TokenType.Semicolon;
                break;
            case "(":
                type = TokenType.OpenBracket;
                break;
            case ")":
                type = TokenType.CloseBracket;
                break;
            default:
                if (int.TryParse(word, out int _))
                {
                    type = TokenType.Integer;
                }
                else if (_identifierRegex.IsMatch(word))
                {
                    type = TokenType.Identifier;
                }
                break;
        }

        Token token = new Token
        {
            Value = word,
            Type = type,
            IsOperator = isOperator
        };

        return token;
    }

    public override string ToString()
    {
        return $"Type: {Type,10} \t\t Value: {Value}";
    }
}
