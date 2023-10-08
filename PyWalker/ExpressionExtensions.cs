

using IronPython.Compiler.Ast;

namespace PyWalker
{
    internal static class ExpressionExtensions
    {
        public static Type? GetCSharpType(this Expression expression)
        {
            if (expression is NameExpression nameExpression)
            {
                return nameExpression.GetCSharpType();
            }

            if (expression is IndexExpression indexExpression)
            {
                return indexExpression.GetCSharpType();
            }

            return null;
        }
    }
}
