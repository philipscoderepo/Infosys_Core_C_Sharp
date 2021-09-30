using System;
using System.Collections.Generic;

namespace CountYourLetters
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("Type a phrase on a single line: ");
            string input = Console.ReadLine();
           
            Dictionary<char, int> letterCount = new Dictionary<char, int>();
            
            //By formatting the whole string to one case, you can effectively ignore the casing when counting
            input = input.ToUpper();

            foreach(char c in input)
            {
                //If the letter already doesn't exist as a key, it will be added in the if statement
                if(letterCount.TryAdd(c, 1))
                {
                    Console.WriteLine($"Not seen '{c}' before, setting it to 1");
                }
                else
                {
                    //add one to the value if the key already exists
                    letterCount[c]++;
                    Console.WriteLine($"Already seen '{c}' before, adding 1 more");
                }
            }
            //output the result
            Console.WriteLine("**************** Done Processing ****************");
            foreach(var item in letterCount)
            {
                Console.WriteLine($"'{item.Key}' was found {item.Value} time(s)");
            }
        }
    }
}
