
class FunctionCallExpression : IExpression
{
    public Token Identifier { get; set; }
    public IExpression ParameterExpression { get; set; }

    public IExpression Accept(ExpressionVisitor visitor)
    {
        return visitor.VisitFunctionCallExpression(this);
    }

    public override string ToString()
    {
        return $"{Identifier.Value} {ParameterExpression};";
    }
}
