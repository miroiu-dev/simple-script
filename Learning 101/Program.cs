string code = @"
print 5^2 * (3 + 15/5);

";

//var code = Console.ReadLine();

List<Token> tokens = new Tokenizer(code).Parse();

List<IExpression> expressions = new ExpressionParser(tokens).Parse();
Interpreter interpreter = new(expressions);
interpreter.Evaluate();
