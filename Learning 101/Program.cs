string code = @"
num x = 5;
num y1 = 10;
num y1 = 12;
print x + y1 - y1;
";

//var code = Console.ReadLine();

List<Token> tokens = new Tokenizer(code).Parse();

List<IExpression> expressions = new ExpressionParser(tokens).Parse();
Interpreter interpreter = new Interpreter(expressions);
interpreter.Evaluate();
