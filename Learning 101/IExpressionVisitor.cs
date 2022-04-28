interface IExpressionVisitor
{
    IExpression VisitBinaryExpression(BinaryExpression binaryExpression);
    IExpression VisitBracketsExpression(BracketsExpression bracketsExpression);
    IExpression VisitConstantExpression(ConstantExpression constantExpression);
    IExpression VisitFunctionCallExpression(FunctionCallExpression functionCallExpression);
    IExpression VisitIdentifierExpression(IdentifierExpression identifierExpression);
    IExpression VisitVariableDeclaration(VariableDeclarationExpression variableDeclaration);
}