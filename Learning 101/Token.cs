using System.Text.RegularExpressions;

class Token
{
    public int Length => Text.Length;
    public TokenType Type { get; set; }
    public string Text { get; set; }
    public int Value { get; set; }
    public bool IsOperator { get; set; }
    public int Line { get; set; }
    public int Column { get; set; }
    public override string ToString()
    {
        return $"Type: {Type,10} \t\t Value: {Text}";
    }
}
