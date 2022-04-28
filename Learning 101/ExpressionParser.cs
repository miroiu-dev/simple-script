class ExpressionParser
{
    private readonly List<Token> _tokens;
    private int _cursor = 0;

    public Token Current => _tokens[_cursor];

    public ExpressionParser(List<Token> tokens)
    {
        _tokens = tokens;
    }

    private Token Peek(int offset = 1)
    {
        return _tokens[_cursor + offset];
    }

    private Token Next()
    {
        Token previous = Current;
        ++_cursor;
        return previous;
    }

    public List<IExpression> Parse()
    {
        List<IExpression> expressions = new();
        while (Current.Type != TokenType.EndOfFile)
        {
            switch (Current.Type)
            {
                case TokenType.Num:
                    VariableDeclarationExpression varDeclaration = ParseVariableDeclaration();
                    expressions.Add(varDeclaration);
                    break;
                case TokenType.Identifier:
                    FunctionCallExpression functionCall = ParseFunctionCallExpression();
                    expressions.Add(functionCall);
                    break;
                default:
                    throw new Exception($"Invalid token type: {Current.Type}");
            }
        }
        return expressions;
    }

    public IExpression ParsePrimaryExpression()
    {
        var nextToken = Peek();
        if (nextToken.IsOperator)
        {
            return ParseBinaryExpression();
        }

        return ParseConstantExpression();
    }

    private BinaryExpression ParseBinaryExpression()
    {
        Token leftOperand = Match(TokenType.Identifier);
        Token operatorToken = Current.IsOperator ? Current : throw new Exception($"Invalid operator: {Current.Type}");
        Next();
        Token rightOperand = Match(TokenType.Identifier);

        return new BinaryExpression { LeftOperand = leftOperand, Operator = operatorToken, RightOperand = rightOperand };
    }

    private ConstantExpression ParseConstantExpression()
    {
        Token constant = Match(TokenType.Integer);
        return new ConstantExpression { Value = constant };
    }

    private VariableDeclarationExpression ParseVariableDeclaration()
    {
        Token dataType = Match(TokenType.Num);
        Token identifier = Match(TokenType.Identifier);
        Match(TokenType.Equals);
        Token value = Match(TokenType.Integer);
        Match(TokenType.Semicolon);
        return new VariableDeclarationExpression { DataType = dataType, Identifier = identifier, Value = value };
    }

    private FunctionCallExpression ParseFunctionCallExpression()
    {
        Token identifier = Match(TokenType.Identifier);

        IExpression expression = ParsePrimaryExpression();

        Match(TokenType.Semicolon);

        return new FunctionCallExpression { Identifier = identifier, ParameterExpression = expression };
    }

    public Token Match(TokenType type)
    {
        if (_tokens.Count > _cursor)
        {
            Token current = _tokens[_cursor];
            _cursor++;
            if (current.Type == type)
            {
                return current;
            }
        }

        throw new Exception($"Expected token of type: {type}");
    }
}

