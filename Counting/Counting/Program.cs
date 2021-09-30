using System;

namespace Counting
{
    class Program
    {
        static string MakeALine(int num)
        {
            string input = $"{num} ";
            System.Text.StringBuilder sb = new System.Text.StringBuilder(50);
            //iterate a number of times equal to the value of num
            for (int i = 0; i < num; i++)
            {
                //this builds each individual character in the string
                sb.Append(input);
            }
            return sb.ToString();
        }

        static string MakeAllLines(int num)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(500);
            //each iteration of the loop calls MakeALine using i to count up
            for(int i = 0; i < num; i++)
            {
                sb.Append(MakeALine(i + 1));
                sb.Append('\n');
            }

            return sb.ToString();
        }

        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("Usage: this program expects to be provided a single integer");
                return;
            }
            int number;
            if(int.TryParse(args[0], out number))
            {
                Console.WriteLine(MakeAllLines(number));
            }
            else
            {
                Console.WriteLine($"Usage: the supplied parameter '{args[0]}' was not a valid integer");
            }

        }
    }
}
