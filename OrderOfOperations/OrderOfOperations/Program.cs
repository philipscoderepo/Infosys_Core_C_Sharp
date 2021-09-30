using System;

namespace OrderOfOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int r = 10;

            Console.WriteLine(Math.PI);
            Console.WriteLine(2 * Math.PI);
            Console.WriteLine($"A Circle with radius 10 jas a circumference of using Math.PI: {2 * Math.PI * r}");
            Console.WriteLine($"A Circle with radius 10 jas a circumference of using 6.28318...: {6.283185307179586 * r}");
            Console.WriteLine($"A Circle with radius 10 jas a circumference of using 355/133: {2 * (355/113) * r}");
            //This last computation is incorrect because the result is cast as an integer. This is because the datatypes of the numbers
            //in the calculation are in integer form. To fix this you could make 2 to 2.0.
        }
    }
}
