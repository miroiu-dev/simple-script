class ExpressionVisitor
{
    private readonly Dictionary<string, double> _variables = new();

    public IExpression VisitIdentifierExpression(IdentifierExpression identifierExpression)
    {
        if (_variables.TryGetValue(identifierExpression.Value.Text, out double integer))
            return new ValueExpression { Value = integer };

        throw new Exception($"Variable {identifierExpression.Value} was not declared in this scope");
    }

    public IExpression VisitBracketsExpression(BracketsExpression bracketsExpression)
    {
        return bracketsExpression.Inner.Accept(this);
    }

    public IExpression VisitVariableDeclaration(VariableDeclarationExpression variableDeclaration)
    {
        ValueExpression value = (ValueExpression)variableDeclaration.Value.Accept(this);
        _variables.Add(variableDeclaration.Identifier.Text, value.Value);
        return new VoidExpression();
    }

    public IExpression VisitBinaryExpression(BinaryExpression binaryExpression)
    {
        ValueExpression leftOperand = (ValueExpression)binaryExpression.LeftOperand.Accept(this);
        ValueExpression rightOperand = (ValueExpression)binaryExpression.RightOperand.Accept(this);

        switch (binaryExpression.Operator.Type)
        {
            case TokenType.Plus:
                double sum = leftOperand.Value + rightOperand.Value;
                return new ValueExpression { Value = sum };
            case TokenType.Minus:
                double diff = leftOperand.Value - rightOperand.Value;
                return new ValueExpression { Value = diff };
            case TokenType.Asterisk:
                double product = leftOperand.Value * rightOperand.Value;
                return new ValueExpression { Value = product };
            case TokenType.Slash:
                double division = leftOperand.Value / rightOperand.Value;
                return new ValueExpression { Value = division };
            case TokenType.Carret:
                double power = (int)Math.Pow(leftOperand.Value, rightOperand.Value);
                return new ValueExpression { Value = power };
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
