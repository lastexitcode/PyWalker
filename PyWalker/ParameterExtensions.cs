

using IronPython.Compiler.Ast;
using System.Text;

namespace PyWalker
{
    static class ParameterExtensions
    {
        public static Type? GetParameterType(this Parameter parameter)
        {
            return parameter.Annotation.GetCSharpType();
        }

        public static string GetCSharpParameterName(this Parameter parameter)
        {
            var builder = new StringBuilder(parameter.Name.Length);

            string[] parts = parameter.Name.Split('_');
            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                if (i == 0)
                {
                    builder.Append(part);
                }
                else
                {
                    builder.Append(char.ToUpper(part[0]));
                    builder.Append(part[1..]);
                }
            }

            return builder.ToString();
        }
    }
}
