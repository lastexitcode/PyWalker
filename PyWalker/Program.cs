
using PyWalker;

if (args.Length == 0)
{
    Console.WriteLine("Python filename not supplied.");
    return;
}

try
{
    var walker = new PythonAstWalker();
    walker.WalkFile(args[0]);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}


