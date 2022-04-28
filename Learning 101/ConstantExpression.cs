class ConstantExpression : IExpression
{
    public Token Value { get; set; }

    public IExpression Accept(ExpressionVisitor visitor)
    {
        return visitor.VisitConstantExpression(this);
    }

    public override string ToString()
    {
        return $"{Value.Value}";
    }
}