
//var code = Console.ReadLine();


class ExpressionVisitor
{
    private readonly Dictionary<string, int> _variables = new();

    public IExpression VisitVariableDeclaration(VariableDeclarationExpression variableDeclaration)
    {
        _variables.Add(variableDeclaration.Identifier.Text, int.Parse(variableDeclaration.Value.Text));
        return new VoidExpression();
    }

    public IExpression VisitBinaryExpression(BinaryExpression binaryExpression)
    {
        Token leftOperand = binaryExpression.LeftOperand;
        Token rightOperand = binaryExpression.RightOperand;

        switch (binaryExpression.Operator.Type)
        {
            case TokenType.Plus:
                int sum = _variables[leftOperand.Text] + _variables[rightOperand.Text];
                return new ValueExpression { Value = sum };
            case TokenType.Minus:
                int diff = _variables[leftOperand.Text] - _variables[rightOperand.Text];
                return new ValueExpression { Value = diff };
            default:
                throw new Exception($"Operator Type: {binaryExpression.Operator.Type} Not Implemented");
        }
    }

    public IExpression VisitConstantExpression(ConstantExpression constantExpression)
    {
        return new ValueExpression { Value = int.Parse(constantExpression.Value.Text) };
    }

    public IExpression VisitFunctionCallExpression(FunctionCallExpression functionCallExpression)
    {
        if (functionCallExpression.Identifier.Text == "print")
        {
            ValueExpression parameter = (ValueExpression)functionCallExpression.ParameterExpression.Accept(this);
            Console.WriteLine(parameter.Value);
            return new VoidExpression();
        }

        throw new Exception($"Function {functionCallExpression.Identifier.Text} does not exist");
    }
}
