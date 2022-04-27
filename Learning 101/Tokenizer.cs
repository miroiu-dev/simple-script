class Tokenizer
{
    public static List<Token> Parse(string code)
    {
        List<Token> tokens = new();
        string[] lines = code.Split(Environment.NewLine);
        foreach (string line in lines)
        {
            string[] words = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                Token token = Token.Create(word);
                tokens.Add(token);
            }
        }
        tokens.Add(Token.Create("\0"));
        return tokens;
    }
}
