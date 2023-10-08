using IronPython.Compiler;
using IronPython.Compiler.Ast;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting.Runtime;
using System.Text;

namespace PyWalker
{
    internal class PythonAstWalker : PythonWalker
    {
        IOutputWriter writer;

        internal PythonAstWalker()
            : this(new ConsoleOutputWriter())
        {
        }

        internal PythonAstWalker(IOutputWriter writer)
        {
            this.writer = writer;
        }

        internal void WalkFile(string fileName)
        {
            SharedPythonEngine.Initialize();

            string content = File.ReadAllText(fileName);
            SourceUnit sourceUnit = DefaultContext.DefaultPythonContext.CreateFileUnit(fileName, content);
            var options = new PythonCompilerOptions();
            var errorSink = new ErrorCounter();
            var parserSink = new ParserSink();
            var context = new CompilerContext(sourceUnit, options, errorSink, parserSink);

            var pythonOptions = new PythonOptions();

            using Parser parser = Parser.CreateParser(context, pythonOptions);

            PythonAst ast = parser.ParseFile(makeModule: false);
           
            ast.Walk(this);
        }

        public override bool Walk(AndExpression node)
        {
            writer.WriteLine("And");
            return base.Walk(node);
        }

        public override bool Walk(AssertStatement node)
        {
            writer.WriteLine("Assert");
            return base.Walk(node);
        }

        public override bool Walk(AugmentedAssignStatement node)
        {
            writer.WriteLine("AugmentedAssignStatement");
            return base.Walk(node);
        }

        public override bool Walk(AssignmentStatement node)
        {
            writer.WriteLine("AssignmentStatement");
            return base.Walk(node);
        }

        public override bool Walk(BinaryExpression node)
        {
            writer.WriteLine("Binary");
            return base.Walk(node);
        }

        public override bool Walk(BreakStatement node)
        {
            writer.WriteLine("Breaks");
            return base.Walk(node);
        }

        public override bool Walk(ClassDefinition node)
        {
            if (node.Bases.Count > 0)
            {
                writer.WriteLine("Class: " + node.Name + " BaseTypes: " + GetBaseTypes(node.Bases));
            }
            else
            {
                writer.WriteLine("Class: " + node.Name);
            }
            return base.Walk(node);
        }

        public override bool Walk(ConditionalExpression node)
        {
            writer.WriteLine("ConditionalExpression");
            return base.Walk(node);
        }

        public override bool Walk(ConstantExpression node)
        {
            writer.WriteLine("ConstantExpression");
            return base.Walk(node);
        }

        public override bool Walk(ContinueStatement node)
        {
            writer.WriteLine("Continue");
            return base.Walk(node);
        }

        public override bool Walk(FunctionDefinition node)
        {
            writer.WriteLine("FunctionDefinition: " + node.Name);
            if (node.Body?.Documentation is not null)
            {
                writer.WriteLine("   doc:\n" + node.Body.Documentation);
            }

            if (node.ReturnAnnotation is NameExpression nameExpression)
            {
                writer.WriteLine("    returns: " + nameExpression.Name);
            }
            return base.Walk(node);
        }

        public override bool Walk(CallExpression node)
        {
            writer.WriteLine("Call");
            return base.Walk(node);
        }

        public override bool Walk(DictionaryExpression node)
        {
            writer.WriteLine("Dict");
            return base.Walk(node);
        }

        public override bool Walk(DottedName node)
        {
            writer.WriteLine("DottedName");
            return base.Walk(node);
        }

        public override bool Walk(ExpressionStatement node)
        {
            writer.WriteLine("Expr");
            return base.Walk(node);
        }

        public override bool Walk(GlobalStatement node)
        {
            writer.WriteLine("Global");
            return base.Walk(node);
        }

        public override bool Walk(NameExpression node)
        {
            writer.WriteLine("Name: " + node.Name);
            return base.Walk(node);
        }

        public override bool Walk(MemberExpression node)
        {
            writer.WriteLine("Member: " + node.Name);
            return base.Walk(node);
        }

        public override bool Walk(FromImportStatement node)
        {
            writer.WriteLine("FromImport: " + node.Root.MakeString());
            foreach (var name in node.Names)
            {
                writer.WriteLine("    " + name);
            }
            return base.Walk(node);
        }

        public override bool Walk(ImportStatement node)
        {
            writer.WriteLine("Import: " + GetImports(node.Names));
            return base.Walk(node);
        }

        public override bool Walk(IndexExpression node)
        {
            writer.WriteLine("Index: " + node.Index.ToString());
            return base.Walk(node);
        }

        public override bool Walk(UnaryExpression node)
        {
            writer.WriteLine("Unary");
            return base.Walk(node);
        }

        public override bool Walk(SuiteStatement node)
        {
            writer.WriteLine("Suite");
            return base.Walk(node);
        }

        public override bool Walk(ErrorExpression node)
        {
            writer.WriteLine("Error");
            return base.Walk(node);
        }

        public override bool Walk(IfStatement node)
        {
            writer.WriteLine("If");
            return base.Walk(node);
        }

        string GetImports(IList<DottedName> names)
        {
            StringBuilder s = new StringBuilder();
            foreach (DottedName name in names)
            {
                s.Append(name.MakeString());
                s.Append(',');
            }
            return s.ToString();
        }

        string GetBaseTypes(IReadOnlyList<Expression> types)
        {
            var s = new StringBuilder();
            foreach (Expression expression in types)
            {
                var nameExpression = expression as NameExpression;
                if (nameExpression != null)
                {
                    s.Append(nameExpression.Name.ToString());
                    s.Append(',');
                }
            }
            return s.ToString();
        }

        public override bool Walk(DictionaryComprehension node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(FormattedValueExpression node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(GeneratorExpression node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(JoinedStringExpression node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(LambdaExpression node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(ListComprehension node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(ListExpression node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(OrExpression node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(ParenthesisExpression node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(SetComprehension node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(SliceExpression node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(StarredExpression node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(TupleExpression node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(YieldFromExpression node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(AnnotatedAssignStatement node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(AsyncStatement node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(DelStatement node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(EmptyStatement node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(ForStatement node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(NonlocalStatement node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(PythonAst node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(RaiseStatement node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(ReturnStatement node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(TryStatement node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(WhileStatement node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(WithStatement node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(ComprehensionFor node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(ComprehensionIf node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(IfStatementTest node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(Keyword node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(ModuleName node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(Parameter node)
        {
            writer.WriteLine(node.GetType().Name + " " + node.Name);
            return base.Walk(node);
        }

        public override bool Walk(RelativeModuleName node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }

        public override bool Walk(TryStatementHandler node)
        {
            writer.WriteLine(node.GetType().Name);
            return base.Walk(node);
        }
    }
}
