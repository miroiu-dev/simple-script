using System.Text;

class CodeGenerator
{
    public static string Generate(List<IExpression> expressions)
    {
        StringBuilder code = new StringBuilder();

        foreach (IExpression expression in expressions)
        {
            code.AppendLine(expression.ToString());
        }

        return code.ToString();
    }
}
