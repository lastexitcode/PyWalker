using IronPython.Hosting;

namespace PyWalker.Tests
{
    public class HumanEvalDataSetTests
    {
        [Fact]
        public void Parse_HasCloseElementsTestProblem()
        {
            string python =
                """"
                from typing import List

                def has_close_elements(numbers: List[float], threshold: float) -> bool:
                    """ Check if in given list of numbers, are any two numbers closer to each other than
                    given threshold.
                    >>> has_close_elements([1.0, 2.0, 3.0], 0.5)
                    False
                    >>> has_close_elements([1.0, 2.8, 3.0, 4.0, 5.0, 2.0], 0.3)
                    True
                    """

                """";

            var parser = new HumanEvalPythonTestProblemParser();
            parser.Parse(python);

            Assert.Equal("HasCloseElements", parser.MethodName);
            Assert.Equal(
                " Check if in given list of numbers, are any two numbers closer to each other than\n    given threshold.",
                parser.MethodDocumentationSummary);
            Assert.Equal(typeof(bool), parser.MethodReturnType);
            Assert.Equal(2, parser.MethodParameters.Length);
            Assert.Equal("numbers", parser.MethodParameters[0].Name);
            Assert.Equal(typeof(List<double>), parser.MethodParameters[0].Type);
            Assert.Equal("threshold", parser.MethodParameters[1].Name);
            Assert.Equal(typeof(double), parser.MethodParameters[1].Type);
            Assert.Contains("System.Collections.Generic", parser.Usings);
        }

        [Fact]
        public void Parse_ListOfStrings()
        {
            string python =
                """
                from typing import List
                
                def separate_paren_groups(paren_string: str) -> List[str]:

                """;

            var parser = new HumanEvalPythonTestProblemParser();
            parser.Parse(python);

            Assert.Equal("SeparateParenGroups", parser.MethodName);
            Assert.Equal(typeof(List<string>), parser.MethodReturnType);
            Assert.Single(parser.MethodParameters);
            Assert.Equal("parenString", parser.MethodParameters[0].Name);
            Assert.Equal(typeof(string), parser.MethodParameters[0].Type);
        }

        [Fact]
        public void Parse_IntAndListOfInts()
        {
            string python =
                """
                from typing import List
                
                def intersperse(numbers: List[int], delimeter: int) -> List[int]:

                """;

            var parser = new HumanEvalPythonTestProblemParser();
            parser.Parse(python);

            Assert.Equal("Intersperse", parser.MethodName);
            Assert.Equal(typeof(List<int>), parser.MethodReturnType);
            Assert.Equal(2, parser.MethodParameters.Length);
            Assert.Equal("numbers", parser.MethodParameters[0].Name);
            Assert.Equal(typeof(List<int>), parser.MethodParameters[0].Type);
            Assert.Equal("delimeter", parser.MethodParameters[1].Name);
            Assert.Equal(typeof(int), parser.MethodParameters[1].Type);
        }

        [Fact]
        public void Parse_TupleOfTwoDoubles()
        {
            string python =
                """
                from typing import List, Tuple
                
                def find_closest_elements(numbers: List[float]) -> Tuple[float, float]:

                """;

            var parser = new HumanEvalPythonTestProblemParser();
            parser.Parse(python);

            Assert.Equal("FindClosestElements", parser.MethodName);
            Assert.Equal(typeof(Tuple<double, double>), parser.MethodReturnType);
            Assert.Single(parser.MethodParameters);
            Assert.Equal("numbers", parser.MethodParameters[0].Name);
            Assert.Equal(typeof(List<double>), parser.MethodParameters[0].Type);
        }

        [Fact]
        public void Parse_TupleOfTwoInts()
        {
            string python =
                """
                from typing import List, Tuple
                
                def sum_product(numbers: List[int]) -> Tuple[int, int]:

                """;

            var parser = new HumanEvalPythonTestProblemParser();
            parser.Parse(python);

            Assert.Equal("SumProduct", parser.MethodName);
            Assert.Equal(typeof(Tuple<int, int>), parser.MethodReturnType);
            Assert.Single(parser.MethodParameters);
            Assert.Equal("numbers", parser.MethodParameters[0].Name);
            Assert.Equal(typeof(List<int>), parser.MethodParameters[0].Type);

        }
    }
}