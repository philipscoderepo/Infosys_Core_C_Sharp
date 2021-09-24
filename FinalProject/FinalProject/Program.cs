using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject
{
    class Program
    {
        static void CalculateTaxes()
        {
            //Calculate Taxes 
            //Prompt the user
            Console.WriteLine("Would you like to compute some individual taxes? 'Y'/'N'");
            if (Console.ReadLine()[0] != 'Y')
            {
                return;
            }
            //check to see if the user wants to use verbose mode
            Console.WriteLine("Turn 'Verbose Mode' on? 'Y'/'N'");
            if (Console.ReadLine()[0] == 'Y')
            {
                TaxCalculator.Verbose = true;
            }
            else
            {
                TaxCalculator.Verbose = false;
            }
            
            while (true)
            {
                //Input validation
                Console.WriteLine("Enter a state abreviation in all caps: ");
                string search = Console.ReadLine();
                if (TaxCalculator.States.ContainsKey(search))
                {
                    Console.WriteLine("Enter the amount earned this year: ");
                    string earned = Console.ReadLine();
                    try
                    {
                        //if the compute is successful, it will write to the console, if not the error will be caught.
                        Console.WriteLine($"Total tax due this year is ${TaxCalculator.ComputeTaxFor(search, earned)}\n");
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {search} was not in the list of states, or contained an error in the data");
                    continue;
                }
                Console.WriteLine("Would you like to compute some more individual taxes? 'Y'/'N'");
                if (Console.ReadLine()[0] != 'Y')
                {
                    break;
                }
            }
            TaxCalculator.Verbose = false;
        }

        static void DisplayQuery(IEnumerable<EmployeeRecord> q)
        {
            Console.WriteLine('\n');
            foreach (EmployeeRecord e in q)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine(String.Empty.PadLeft(50, '*'));
        }

        static void QueryRecords()
        {
            Console.WriteLine("Would you like to sort the records? 'Y'");
            if (Console.ReadLine()[0] != 'Y')
            {
                return;
            }
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("Choose how to sort the list of employee records: ");
                Console.WriteLine("By State:         'ST'");
                Console.WriteLine("By Employee ID:   'ID'");
                Console.WriteLine("By Employee Name: 'EN'");
                Console.WriteLine("By Yearly Pay:    'YP'");
                Console.WriteLine("By Tax Due:       'TD'");
                Console.WriteLine(String.Empty.PadLeft(50, '-'));
                //Generate the query using all the records
                IEnumerable<EmployeeRecord> allRecords = from e in EmployeeList.EmployeeRecords select e;
                //Then use the user input to choose how to sort the records
                switch (Console.ReadLine())
                {
                    case "ST":
                        Console.WriteLine("Sort States by Ascending or Descending? 'A'/'D'");
                        if (Console.ReadLine()[0] == 'A')
                        {
                            DisplayQuery(allRecords.OrderBy(e => e.State));
                        }
                        else
                        {
                            DisplayQuery(allRecords.OrderByDescending(e => e.State));
                        }
                        break;
                    case "ID":
                        Console.WriteLine("Sort ID by Ascending or Descending? 'A'/'D'");
                        if (Console.ReadLine()[0] == 'A')
                        {
                            DisplayQuery(allRecords.OrderBy(e => e.ID));
                        }
                        else
                        {
                            DisplayQuery(allRecords.OrderByDescending(e => e.ID));
                        }
                        break;
                    case "EN":
                        Console.WriteLine("Sort Employee Name by Ascending or Descending? 'A'/'D'");
                        if (Console.ReadLine()[0] == 'A')
                        {
                            DisplayQuery(allRecords.OrderBy(e => e.Name));
                        }
                        else
                        {
                            DisplayQuery(allRecords.OrderByDescending(e => e.Name));
                        }
                        break;
                    case "YP":
                        Console.WriteLine("Sort Yearly Pay by Ascending or Descending? 'A'/'D'");
                        if (Console.ReadLine()[0] == 'A')
                        {
                            DisplayQuery(allRecords.OrderBy(e => e.YearlyPay));
                        }
                        else
                        {
                            DisplayQuery(allRecords.OrderByDescending(e => e.YearlyPay));
                        }
                        break;
                    case "TD":
                        Console.WriteLine("Sort Tax Due by Ascending or Descending? 'A'/'D'");
                        if (Console.ReadLine()[0] == 'A')
                        {
                            DisplayQuery(allRecords.OrderBy(e => e.TaxDue));
                        }
                        else
                        {
                            DisplayQuery(allRecords.OrderByDescending(e => e.TaxDue));
                        }
                        break;
                    default: 
                        Console.WriteLine("\nError: Invalid input\n");
                        continue;
                }
                Console.WriteLine("Would you like to sort again? 'Y'");
                if (Console.ReadLine()[0] != 'Y')
                {
                    break;
                }
                Console.WriteLine();
            } 
        }

        static void Main(string[] args)
        {
            //Calculate taxes will call the method above which will get the data to 
            //calculate the taxes for the individual.
            CalculateTaxes();

            //Display Employee Records
            foreach(EmployeeRecord e in EmployeeList.EmployeeRecords)
            {
                Console.WriteLine(e);
            }

            //Sort using LINQ
            QueryRecords();

            Console.WriteLine("Goodbye!");
        }
    }
}
