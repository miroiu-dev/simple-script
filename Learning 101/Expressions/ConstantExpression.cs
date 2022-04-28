class ConstantExpression : IExpression
{
    public Token Value { get; set; }

    public IExpression Accept(IExpressionVisitor visitor)
    {
        return visitor.VisitConstantExpression(this);
    }

    public override string ToString()
    {
        return Value.Type == TokenType.Text ? $"'{Value.Text}'" : $"{Value.Text}";
    }
}