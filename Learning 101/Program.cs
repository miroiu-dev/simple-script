string code = @"
num x = 5 ;
num y = 10 ;
print x + y ;
";

//var code = Console.ReadLine();

List<Token> tokens = Tokenizer.Parse(code);

List<IExpression> expressions = new ExpressionParser(tokens).Parse();
Interpreter interpreter = new Interpreter(expressions);
interpreter.Evaluate();

class ExpressionVisitor
{
    private readonly Dictionary<string, int> _variables = new();

    public IExpression VisitVariableDeclaration(VariableDeclarationExpression variableDeclaration)
    {
        _variables.Add(variableDeclaration.Identifier.Value, int.Parse(variableDeclaration.Value.Value));
        return new VoidExpression();
    }

    public IExpression VisitBinaryExpression(BinaryExpression binaryExpression)
    {
        Token leftOperand = binaryExpression.LeftOperand;
        Token rightOperand = binaryExpression.RightOperand;

        switch (binaryExpression.Operator.Type)
        {
            case TokenType.Plus:
                int sum = _variables[leftOperand.Value] + _variables[rightOperand.Value];
                return new ValueExpression { Value = sum };
            case TokenType.Minus:
                int diff = _variables[leftOperand.Value] - _variables[rightOperand.Value];
                return new ValueExpression { Value = diff };
            default:
                throw new Exception($"Operator Type: {binaryExpression.Operator.Type} Not Implemented");
        }
    }

    public IExpression VisitConstantExpression(ConstantExpression constantExpression)
    {
        return new ValueExpression { Value = int.Parse(constantExpression.Value.Value) };
    }

    public IExpression VisitFunctionCallExpression(FunctionCallExpression functionCallExpression)
    {
        if (functionCallExpression.Identifier.Value == "print")
        {
            ValueExpression parameter = (ValueExpression)functionCallExpression.ParameterExpression.Accept(this);
            Console.WriteLine(parameter.Value);
            return new VoidExpression();
        }

        throw new Exception($"Function {functionCallExpression.Identifier.Value} does not exist");
    }
}

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
