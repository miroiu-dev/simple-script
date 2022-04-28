class VoidExpression : IExpression
{
    public IExpression Accept(ExpressionVisitor visitor)
    {
        return this;
    }

    public override string ToString()
    {
        return string.Empty;
    }
}
