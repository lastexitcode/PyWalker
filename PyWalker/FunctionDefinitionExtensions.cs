using System.Text;
using IronPython.Compiler.Ast;

namespace PyWalker
{
    internal static class FunctionDefinitionExtensions
    {
        public static string GetCSharpMethodName(this FunctionDefinition definition)
        {
            var builder = new StringBuilder(definition.Name.Length);

            string[] parts = definition.Name.Split('_');
            foreach (string part in parts)
            {
                builder.Append(char.ToUpper(part[0]));
                builder.Append(part[1..]);
            }

            return builder.ToString();
        }

        public static string GetDocumentationSummary(this FunctionDefinition definition)
        {
            if (string.IsNullOrEmpty(definition.Body.Documentation))
            {
                return string.Empty;
            }


            int index = definition.Body.Documentation.IndexOf(">>>");
            if (index < 0)
            {
                return definition.Body.Documentation;
            }

            // Remove examples for now since these are written in python.
            return definition.Body.Documentation
                .Substring(0, index)
                .TrimEnd();
        }

        public static Type? GetReturnType(this FunctionDefinition definition)
        {
            return definition.ReturnAnnotation.GetCSharpType();
        }
    }
}
