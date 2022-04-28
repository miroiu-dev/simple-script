class ValueExpression : IExpression
{
    public double? Value { get; set; }
    public string? Text {  get; set; }

    public bool IsNumber => Value.HasValue;

    public IExpression Accept(IExpressionVisitor visitor)
    {
        return this;
    }

    public override string? ToString()
    {
        return Value?.ToString() ?? $"'{Text}'";
    }
}
