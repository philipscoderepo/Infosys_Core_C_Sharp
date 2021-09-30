using System;

namespace EmployeePractice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the First Integer:");
            string input = Console.ReadLine();
            int firstNum = Int32.Parse(input);

            Console.WriteLine("Enter the Second Integer:");
            input = Console.ReadLine();
            int secondNum = Int32.Parse(input);

            Console.WriteLine($"The sum of {firstNum} + {secondNum} = {firstNum + secondNum}");
        }
    }
}
