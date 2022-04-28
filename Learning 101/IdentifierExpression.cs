class IdentifierExpression : IExpression
{
    public Token Value { get; set; }

    public IExpression Accept(ExpressionVisitor visitor)
    {
        return visitor.VisitIdentifierExpression(this);
    }
}
