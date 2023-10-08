using IronPython.Compiler.Ast;

namespace PyWalker
{
    static class NameExpressionExtensions
    {
        public static Type? GetCSharpType(this NameExpression expression)
        {
            switch (expression.Name)
            {
                case "bool":
                    return typeof(bool);
                case "float":
                    return typeof(double);
                case "str":
                    return typeof(string);
                case "int":
                    return typeof(int);
            }

            return null;
        }

        public static Type? GetGenericCSharpType(
            this NameExpression expression,
            NameExpression genericTypeParameterExpression)
        {
            switch (expression.Name)
            {
                case "List":
                    return GetGenericListType(genericTypeParameterExpression);
            }

            return null;
        }

        private static Type? GetGenericListType(NameExpression expression)
        {
            switch (expression.Name)
            {
                case "float":
                    return typeof(List<double>);
                case "str":
                    return typeof(List<string>);
                case "int":
                    return typeof(List<int>);
            }

            return null;
        }
    }
}
