
class VariableDeclarationExpression : IExpression
{
    public Token DataType { get; set; }
    public Token Identifier { get; set; }
    public IExpression Value { get; set; }

    public IExpression Accept(ExpressionVisitor visitor)
    {
        return visitor.VisitVariableDeclaration(this);
    }

    public override string ToString()
    {
        return $"{DataType.Text} {Identifier.Text} = {Value};";
    }
}
