class Interpreter
{
    private readonly List<IExpression> _expressions;

    public Interpreter(List<IExpression> expressions)
    {
        _expressions = expressions;
    }

    public void Evaluate()
    {
        ExpressionVisitor visitor = new ExpressionVisitor();

        foreach (IExpression expression in _expressions)
        {
            expression.Accept(visitor);
        }
    }
}
