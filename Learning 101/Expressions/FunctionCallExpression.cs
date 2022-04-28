
class FunctionCallExpression : IExpression
{
    public Token Identifier { get; set; }
    public IExpression ParameterExpression { get; set; }

    public IExpression Accept(IExpressionVisitor visitor)
    {
        return visitor.VisitFunctionCallExpression(this);
    }

    public override string ToString()
    {
        return $"{Identifier.Text} {ParameterExpression};";
    }
}
