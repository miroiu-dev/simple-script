class ExpressionParser
{
    private readonly List<Token> _tokens;
    private int _cursor = 0;
    private readonly Dictionary<TokenType, int> _precedence = new()
    {
        [TokenType.Plus] = 0,
        [TokenType.Minus] = 0,
        [TokenType.Slash] = 1,
        [TokenType.Asterisk] = 1,
        [TokenType.Carret] = 2,
    };

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
                case TokenType.Str:
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

        return ParseLeafExpression();
    }

    public IExpression ParseBracketsExpression()
    {
        Match(TokenType.OpenBracket);
        IExpression inner = ParseBinaryExpression();
        Match(TokenType.CloseBracket);

        return new BracketsExpression { Inner = inner };
    }

    private IExpression ParseLeafExpression()
    {
        return Current.Type switch
        {
            TokenType.Identifier => ParseIdentifierExpression(),
            TokenType.Integer => ParseConstantExpression(),
            TokenType.Text => ParseConstantExpression(),
            TokenType.OpenBracket => ParseBracketsExpression(),
            _ => throw new Exception($"Unexpected token type: {Current.Type}"),
        };
    }

    private IExpression ParseBinaryExpression(int? parentPrecedence = default)
    {
        IExpression left = ParseLeafExpression();

        while (Current.Type != TokenType.Semicolon && Current.Type != TokenType.EndOfFile)
        {
            if (Current.IsOperator)
            {
                int precedence = _precedence[Current.Type];
                if (parentPrecedence >= precedence)
                {
                    return left;
                }

                Token operatorToken = Next();
                left = new BinaryExpression { LeftOperand = left, Operator = operatorToken, RightOperand = ParseBinaryExpression(parentPrecedence: precedence) };
            }
            else
            {
                return left;
            }
        }

        return left;
    }

    private IExpression ParseIdentifierExpression()
    {
        Token token = Match(TokenType.Identifier);
        return new IdentifierExpression
        {
            Value = token
        };
    }

    private ConstantExpression ParseConstantExpression()
    {
        Token constant = Match(Current.Type == TokenType.Integer ? TokenType.Integer : TokenType.Text);
        return new ConstantExpression { Value = constant };
    }

    private VariableDeclarationExpression ParseVariableDeclaration()
    {
        Token dataType = Match(Current.Type == TokenType.Num ? TokenType.Num : TokenType.Str);
        Token identifier = Match(TokenType.Identifier);
        Match(TokenType.Equals);
        IExpression value = ParsePrimaryExpression();
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

