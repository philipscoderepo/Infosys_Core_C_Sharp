using System;
using System.IO;
using System.Collections.Generic;

namespace SalesTracker
{
    class Sale
    {
        string employee;
        decimal salesAmount;
        decimal commissionRate;

        public string Employee
        {
            get { return employee; }
            set { employee = value; }
        }

        public decimal SalesAmount
        {
            get { return salesAmount; }
            set { salesAmount = value; }
        }

        decimal CommissionRate
        {
            get { return commissionRate; }
            set { commissionRate = value; }
        }

        public decimal Commission
        {
            get { return salesAmount * commissionRate; }
        }

        public Sale(string employee, decimal salesAmount, decimal commissionRate)
        {
            Employee = employee;
            SalesAmount = salesAmount;
            CommissionRate = commissionRate;
        }

        public Sale(string employee, decimal salesAmount)
        {
            Employee = employee;
            SalesAmount = salesAmount;
            CommissionRate = .03M;
        }

        public Sale(string line)
        {
            string[] data = line.Split(',');
            string newEmp;
            decimal newSale;
            decimal newCommRate;
            
            //Verify the data
            if(data.Length != 3) { throw new Exception("Not enough commas"); }
            if(data[0] == null) { throw new Exception("Employee name is null"); }
            else { newEmp = data[0]; }
            if(!decimal.TryParse(data[1], out newSale)) { throw new Exception("Invalid sale amount"); }
            if(!decimal.TryParse(data[2], out newCommRate)) { throw new Exception("Invalid commission rate"); }
            //Store the new data in the sales instance
            Employee = newEmp;
            SalesAmount = newSale;
            CommissionRate = newCommRate;
        }

        public override string ToString()
        {
            return $"Employee: {Employee, 10} Sales: {decimal.Round(SalesAmount, 2).ToString("0000"), 15} " +
                $"Commission Rate: {decimal.Round(CommissionRate, 3).ToString("0.000"), 10} " +
                $"Commission: {decimal.Round(Commission, 2), 10}";
        }

        public static Sale operator +(Sale l, Sale r)
        {
            if(l.Employee != r.Employee)
            {
                throw new Exception($"+ operator only works if employees are the same, {l.Employee} != {r.Employee}");
            }

            //Use the left employee for the name, add the salesAmounts, and calculate the commission rate
            return new Sale(l.Employee, (l.SalesAmount + r.SalesAmount), (l.Commission + r.Commission) / (l.SalesAmount + r.SalesAmount));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Problem 1
            Console.WriteLine($"Problem 1 \n{String.Empty.PadLeft(110, '*')}");
            Sale s1 = new Sale("One", 1000, .10M);
            Sale s2 = new Sale("One", 500, .05M);
            Sale s3 = new Sale("Two", 2500, .10M);
            Sale s4 = new Sale("Two", 3000);

            try
            {
                Console.WriteLine(s1);
                Console.WriteLine(s2);
                Console.WriteLine(s3);
                Console.WriteLine(s4);
                Console.WriteLine(s1 + s2);
                Console.WriteLine(s3 + s4);
                Console.WriteLine(s2 + s3);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //Problem 2
            Console.WriteLine($"\nProblem 2 \n{String.Empty.PadLeft(110, '*')}");

            var file = File.OpenText("sales.csv");
            string line;
            List<Sale> sales = new List<Sale>();

            //Read the lines from the sales.csv file, until the end of file
            while ((line = file.ReadLine()) != null)
            {
                try
                {
                    Sale sale = new Sale(line);
                    sales.Add(sale);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            file.Close();

            int index = 0;
            foreach(Sale s in sales)
            {
                Console.WriteLine($"Index {index}: " + s);
                index++;
            }

            while (true)
            {
                //Get the input from the user
                Console.WriteLine("\nType two indexes from above. Separate the input by a space");
                string input = Console.ReadLine();
                //split the input
                string[] num = input.Split(' ');
                if(num.Length != 2) { Console.WriteLine("Incorrect number of integers"); continue; }
                int one, two;
                //Validate the input
                if(int.TryParse(num[0], out one) && int.TryParse(num[1], out two))
                {
                    if (one > 0 && two > 0)
                    {
                        if (one < sales.Count && two < sales.Count)
                        {
                            Console.WriteLine(String.Empty.PadLeft(110, '*'));
                            Console.WriteLine(sales[one] + sales[two]);
                        }
                        else
                        {
                            Console.WriteLine("Input must be a valid index");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input has to be a positive integer");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Input has to be an integer");
                    continue;
                }
            }
        }
    }
}
