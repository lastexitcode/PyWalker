

using IronPython.Compiler.Ast;

namespace PyWalker
{
    internal static class IndexExpressionExtensions
    {
        public static Type? GetCSharpType(this IndexExpression expression)
        {
            if (expression.Target is NameExpression targetNameExpression)
            {
                if (expression.Index is NameExpression indexNameExpression)
                {
                    return targetNameExpression.GetGenericCSharpType(indexNameExpression);
                }
                else if (expression.Index is TupleExpression tupleExpression)
                {
                    return tupleExpression.GetCSharpType();
                }
            }

            return null;
        }
    }
}
