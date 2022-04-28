var code = File.ReadAllText("code.ss");

List<Token> tokens = new Tokenizer(code).Parse();

List<IExpression> expressions = new ExpressionParser(tokens).Parse();
Interpreter interpreter = new(expressions);
interpreter.Evaluate();

var result = CodeGenerator.Generate(expressions);
Console.WriteLine(result);