class ExpressionVisitor : IExpressionVisitor
{
    private readonly Dictionary<string, double?> _variables = new();
    private readonly Dictionary<string, string?> _strings = new();

    public IExpression VisitIdentifierExpression(IdentifierExpression identifierExpression)
    {
        if (_variables.TryGetValue(identifierExpression.Value.Text, out double? integer))
        {
            return new ValueExpression { Value = integer };
        }
        else if (_strings.TryGetValue(identifierExpression.Value.Text, out string? text))
        {
            return new ValueExpression { Text = text };
        }

        throw new Exception($"Variable {identifierExpression.Value} was not declared in this scope");
    }

    public IExpression VisitBracketsExpression(BracketsExpression bracketsExpression)
    {
        return bracketsExpression.Inner.Accept(this);
    }

    public IExpression VisitVariableDeclaration(VariableDeclarationExpression variableDeclaration)
    {
        if (variableDeclaration.DataType.Type == TokenType.Str)
        {
            ValueExpression value = (ValueExpression)variableDeclaration.Value.Accept(this);
            _strings.Add(variableDeclaration.Identifier.Text, value.Text);
        }
        else
        {
            ValueExpression value = (ValueExpression)variableDeclaration.Value.Accept(this);
            _variables.Add(variableDeclaration.Identifier.Text, value.Value);
        }

        return new VoidExpression();
    }

    public IExpression VisitBinaryExpression(BinaryExpression binaryExpression)
    {
        ValueExpression leftOperand = (ValueExpression)binaryExpression.LeftOperand.Accept(this);
        ValueExpression rightOperand = (ValueExpression)binaryExpression.RightOperand.Accept(this);

        if (leftOperand.IsNumber && rightOperand.IsNumber)
        {
            switch (binaryExpression.Operator.Type)
            {
                case TokenType.Plus:
                    double? sum = leftOperand.Value + rightOperand.Value;
                    return new ValueExpression { Value = sum };
                case TokenType.Minus:
                    double? diff = leftOperand.Value - rightOperand.Value;
                    return new ValueExpression { Value = diff };
                case TokenType.Asterisk:
                    double? product = leftOperand.Value * rightOperand.Value;
                    return new ValueExpression { Value = product };
                case TokenType.Slash:
                    double? division = leftOperand.Value / rightOperand.Value;
                    return new ValueExpression { Value = division };
                case TokenType.Carret:
                    double? power = Math.Pow(leftOperand.Value!.Value, rightOperand.Value!.Value);
                    return new ValueExpression { Value = power };
                default:
                    throw new Exception($"Operator Type: {binaryExpression.Operator.Type} Not Implemented");
            }
        }
        else if (binaryExpression.Operator.Type == TokenType.Plus)
        {
            string? concat = leftOperand.Text + rightOperand.Text;
            return new ValueExpression { Text = concat };
        }

        throw new Exception($"Cannot add numbers to strings");
    }

    public IExpression VisitConstantExpression(ConstantExpression constantExpression)
    {
        if (constantExpression.Value.Type == TokenType.Integer)
        {
            return new ValueExpression { Value = int.Parse(constantExpression.Value.Text) };
        }
        else
        {
            return new ValueExpression { Text = constantExpression.Value.Text };
        }
    }

    public IExpression VisitFunctionCallExpression(FunctionCallExpression functionCallExpression)
    {
        if (functionCallExpression.Identifier.Text == "print")
        {
            ValueExpression parameter = (ValueExpression)functionCallExpression.ParameterExpression.Accept(this);
            Console.WriteLine(parameter.IsNumber ? parameter.Value : parameter.Text);
            return new VoidExpression();
        }

        throw new Exception($"Function {functionCallExpression.Identifier.Text} does not exist");
    }
}
