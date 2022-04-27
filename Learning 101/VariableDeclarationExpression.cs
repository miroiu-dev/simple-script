
class VariableDeclarationExpression : IExpression
{
    public Token DataType { get; set; }
    public Token Identifier { get; set; }
    public Token Value { get; set; }

    public override string ToString()
    {
        return $"{DataType.Value} {Identifier.Value} = {Value.Value};";
    }
}
