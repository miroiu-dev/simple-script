
class FunctionCallExpression : IExpression
{
    public Token Identifier { get; set; }
    public IExpression ParameterExpression { get; set; }

    public override string ToString()
    {
        return $"{Identifier.Value} {ParameterExpression};";
    }
}
