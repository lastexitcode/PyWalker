using IronPython.Compiler.Ast;
using IronPython.Compiler;
using IronPython.Runtime;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting;

namespace PyWalker
{
    public class HumanEvalPythonTestProblemParser : PythonWalker
    {
        public string? MethodName { get; private set; }

        public Type? MethodReturnType { get; private set; }

        public string MethodDocumentationSummary { get; private set; } = string.Empty;

        public ParameterInfo[] MethodParameters { get; private set; } = Array.Empty<ParameterInfo>();

        public string[] Usings { get; private set; } = Array.Empty<string>();

        public void Parse(string pythonCode)
        {
            SharedPythonEngine.Initialize();

            SourceUnit sourceUnit = DefaultContext.DefaultPythonContext.CreateFileUnit(
                @"python.py",
                pythonCode);

            var options = new PythonCompilerOptions();
            var errorSink = new ErrorCounter();
            var parserSink = new ParserSink();
            var context = new CompilerContext(sourceUnit, options, errorSink, parserSink);

            var pythonOptions = new PythonOptions();

            using Parser parser = Parser.CreateParser(context, pythonOptions);

            PythonAst ast = parser.ParseFile(makeModule: false);

            ast.Walk(this);
        }

        public override bool Walk(FunctionDefinition node)
        {
            var usingsSet = new HashSet<string>();

            MethodName = node.GetCSharpMethodName();
            MethodDocumentationSummary = node.GetDocumentationSummary();
            MethodReturnType = node.GetReturnType();

            var parameters = new List<ParameterInfo>(node.Parameters.Count);

            foreach (Parameter parameter in node.Parameters)
            {
                var info = new ParameterInfo
                {
                    Name = parameter.GetCSharpParameterName(),
                    Type = parameter.GetParameterType(),
                };
                parameters.Add(info);

                if (info.Type?.Namespace is not null)
                {
                    usingsSet.Add(info.Type.Namespace);
                }
            }

            MethodParameters = parameters.ToArray();

            Usings = usingsSet.ToArray();

            return false;
        }
    }
}