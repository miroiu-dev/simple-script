
class BinaryExpression : IExpression
{
    public Token LeftOperand { get; set; }
    public Token RightOperand { get; set; }
    public Token Operator { get; set; }

    public IExpression Accept(ExpressionVisitor visitor)
    {
       return visitor.VisitBinaryExpression(this);
    }

    public override string ToString()
    {
        return $"{LeftOperand.Value} {Operator.Value} {RightOperand.Value}";
    }
}
