class IdentifierExpression : IExpression
{
    public Token Value { get; set; }

    public IExpression Accept(IExpressionVisitor visitor)
    {
        return visitor.VisitIdentifierExpression(this);
    }
    public override string ToString()
    {
        return Value.Text;
    }
}
