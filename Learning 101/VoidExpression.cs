class VoidExpression : IExpression
{
    public IExpression Accept(ExpressionVisitor visitor)
    {
        return this;
    }
}
