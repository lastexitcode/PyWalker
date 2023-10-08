

using IronPython.Compiler.Ast;

namespace PyWalker
{
    internal static class TupleExpressionExtensions
    {
        public static Type? GetCSharpType(this TupleExpression expression)
        {
            if (expression.Items.Count == 2)
            {
                return GetTuple2Type(expression);
            }

            return null;
        }

        private static Type? GetTuple2Type(TupleExpression expression)
        {
            var tuple1Expression = expression.Items[0] as NameExpression;
            var tuple2Expression = expression.Items[1] as NameExpression;

            var type1 = tuple1Expression?.GetCSharpType();
            var type2 = tuple2Expression?.GetCSharpType();

            if (type1 != type2)
            {
                return null;
            }

            if (type1 == typeof(double))
            {
                return typeof(Tuple<double, double>);
            }
            else if (type1 == typeof(int))
            {
                return typeof(Tuple<int, int>);
            }

            return null;
        }
    }
}
