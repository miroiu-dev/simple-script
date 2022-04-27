class ConstantExpression : IExpression
{
    public Token Value { get; set; }

    public override string ToString()
    {
        return $"{Value.Value}";
    }
}