
interface IExpression
{
    IExpression Accept(ExpressionVisitor visitor);
}
