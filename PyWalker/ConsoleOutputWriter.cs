namespace PyWalker
{
    class ConsoleOutputWriter : IOutputWriter
    {
        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
    }

}
