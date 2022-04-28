class ValueExpression : IExpression
{
    public int Value { get; set; }
    public IExpression Accept(ExpressionVisitor visitor)
    {
        return this;
    }
}
