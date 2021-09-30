using System;

namespace SumIntegers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter as many integers as you wish separated by spaces, press return when you are ready for the sum:");
            string input = Console.ReadLine(); ;
            //Split the string into an array by using a space as the delimiter
            string[] numbers = input.Split(' ');
            int total = 0;
            int num;
                foreach (string s in numbers)
                {
                    //Try to parse the string to a number
                    try
                    {
                        num = Int32.Parse(s);
                        //if it's sucessful, add to the total
                        total += num;
                        Console.WriteLine($"Accepted: {num}, the total is now {total}");
                    }
                    catch
                    {
                        Console.WriteLine($"Rejected: {s} is an invalid input");
                    }

                }

            Console.WriteLine($"The total final sum of acceptable integers is {total}");
        }
    }
}
