using System;
using MyNamespace;
using UniqueNamespace;

namespace Namespaces
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            AnotherProgram.PrintText();
            AlternateProgram.PrintText();
        }
    }
}
