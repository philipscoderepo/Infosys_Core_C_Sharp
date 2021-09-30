using System;

namespace EmployeeInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "Lucinda Potter",
            dob = "06/24/1992",
            workPhoneNumber = "312-982-1010",
            email = "ewoz@woz-u.com";


            Console.WriteLine($"Name: {name}\n" +
                $"Date of Birth: {dob}\n" +
                $"Extension: {workPhoneNumber}\n" +
                $"Email: {email}\n");
        }
    }
}
