using System;
using System.IO;

namespace ProcessInvoices
{
    class Program
    {

        static decimal ComputeLateFees(DateTime currentDate, DateTime dueDate, decimal amountDue)
        {
            int daysLate = (currentDate - dueDate).Days;
            //if the days late is less than 1, the invoice is not late
            if(daysLate < 1)
            {
                return 0;
            }
            //10% for the first 7 days
            if(daysLate < 8)
            {
                return amountDue * .1M * daysLate;
            }
            //10% for the first 7 days and then 1% for every day after that
            return (amountDue * .1M * 7) + (amountDue * .01M * (daysLate - 7));
        }

        static void Main(string[] args)
        {
            DateTime date;
            //Checks to see if a date was provided as an arg
            if(args.Length == 0)
            {
                date = DateTime.Now;
                Console.WriteLine("No date provided, using current date");
            }
            //Checks to see if the date supplied was on one line of input
            else if(args.Length != 1)
            {
                Console.WriteLine("Argument invalid: only supply one line of input");
                return;
            }
            //Checks to see if the date is correctly formatted
            else if(!DateTime.TryParse(args[0], out date))
            {
                Console.WriteLine($"{args[0]} was not understood as a valid date");
                return;
            }

            Console.WriteLine($"Calculating fees based on {date.ToShortDateString()}:\n");
            var reader = File.OpenText("invoices.dat");
            string line;
            while((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split(",");
                if(data.Length != 3)
                {
                    Console.WriteLine("Invalid data format");
                    continue;
                }

                int invoiceID;
                DateTime invoiceDate;
                decimal invoiceAmount;

                //Check to see if the data is valid
                if(!int.TryParse(data[0], out invoiceID))
                {
                    Console.WriteLine($"{data[0]} is an invalid invoice ID");
                    continue;
                }
                if (!DateTime.TryParse(data[1], out invoiceDate))
                {
                    Console.WriteLine($"{data[1]} is an invalid date");
                    continue;
                }
                if (!decimal.TryParse(data[2], out invoiceAmount))
                {
                    Console.WriteLine($"{data[2]} is an invalid invoice amount");
                    continue;
                }

                //Output the invoices
                Console.WriteLine($"Invoice ID: {invoiceID} " +
                    $"\n************************************************************************************\n" +
                    $"Due Date: {invoiceDate.ToShortDateString()}" +
                    $"   Invoice Amount: {invoiceAmount}" +
                    $"   Late Fees: {ComputeLateFees(date, invoiceDate, invoiceAmount):0.00}\n"); 

            }

            reader.Close();
        }
    }
}
