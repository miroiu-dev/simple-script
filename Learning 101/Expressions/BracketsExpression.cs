class BracketsExpression : IExpression
{
    public IExpression Inner { get; set; }
    public IExpression Accept(IExpressionVisitor visitor)
    {
        return visitor.VisitBracketsExpression(this);
    }

    public override string ToString()
    {
        return $"({Inner})";
    }
}