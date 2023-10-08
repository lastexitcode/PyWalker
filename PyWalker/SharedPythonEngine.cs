using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace PyWalker
{
    internal static class SharedPythonEngine
    {
        static readonly ScriptEngine engine;

        static SharedPythonEngine()
        {
            engine = Python.CreateEngine();
        }

        public static ScriptEngine Engine => engine;

        public static void Initialize()
        {
            // No-op.
        }
    }
}
