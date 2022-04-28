
interface IExpression
{
    IExpression Accept(IExpressionVisitor visitor);
}
