
class BinaryExpression : IExpression
{
    public IExpression LeftOperand { get; set; }
    public IExpression RightOperand { get; set; }
    public Token Operator { get; set; }

    public IExpression Accept(ExpressionVisitor visitor)
    {
        return visitor.VisitBinaryExpression(this);
    }
    public override string ToString()
    {
        return $"{LeftOperand} {Operator.Text} {RightOperand}";
    }
}
