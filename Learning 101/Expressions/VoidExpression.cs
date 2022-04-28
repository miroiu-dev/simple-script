class VoidExpression : IExpression
{
    public IExpression Accept(IExpressionVisitor visitor)
    {
        return this;
    }

    public override string ToString()
    {
        return string.Empty;
    }
}
