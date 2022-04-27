//string code = @"
//num x = 5 ;
//num y = 10 ;
//print x + y ;
//";

var code = Console.ReadLine();

List<Token> tokens = Tokenizer.Parse(code);

List<IExpression> expressions = new ExpressionParser(tokens).Parse();

