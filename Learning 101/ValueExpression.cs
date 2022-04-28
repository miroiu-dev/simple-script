class ValueExpression : IExpression
{
    public double Value { get; set; }
    public IExpression Accept(ExpressionVisitor visitor)
    {
        return this;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
