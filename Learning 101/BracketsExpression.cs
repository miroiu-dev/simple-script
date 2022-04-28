class BracketsExpression : IExpression
{
    public IExpression Inner { get; set; }
    public IExpression Accept(ExpressionVisitor visitor)
    {
        return visitor.VisitBracketsExpression(this);
    }

    public override string ToString()
    {
        return $"({Inner})";
    }
}