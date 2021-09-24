using System;
using System.IO;
using System.Collections.Generic;

namespace FinalProject
{
    public class Tax
    {
        int floor;
        long ceiling;
        decimal taxRate;

        public Tax()
        {
            //
            floor = 0;
            ceiling = 0;
            taxRate = 0;
        }

        public Tax(int floor, long ceiling, decimal taxRate)
        {
            Floor = floor;
            Ceiling = ceiling;
            TaxRate = taxRate;
        }

        public int Floor
        {
            get { return floor; }
            set { floor = value; }
        }

        public long Ceiling
        {
            get { return ceiling; }
            set { ceiling = value; }
        }

        public decimal TaxRate
        {
            get { return taxRate; }
            set { taxRate = value; }
        }
    }

    public class State
    {
        string abrvName;
        string fullName;
        List<Tax> taxes;

        public State()
        {
            //
            AbvrName = null;
            fullName = null;
            Taxes = new List<Tax>();
        }

        public State(string abrvName, string fullName, List<Tax> taxes, bool isValid)
        {
            AbvrName = abrvName;
            FullName = fullName;
            Taxes = taxes;
        }

        public string AbvrName
        {
            get { return abrvName; }
            set { abrvName = value; }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public List<Tax> Taxes
        {
            get { return taxes; }
            set { taxes = value; }
        }

    }

    public class EmployeeRecord
    {
        int id;
        string name;
        decimal yearlyPay;
        string state;
        decimal taxDue;

        public EmployeeRecord(int id, string name, decimal yearlyPay, string state)
        {
            ID = id;
            Name = name;
            YearlyPay = yearlyPay;
            State = state;
            try
            {
                TaxDue = TaxCalculator.ComputeTaxFor(state, yearlyPay.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public override string ToString()
        {
            string output = "Employee ID: " + id.ToString() + '\n';
            output += "Employee Name: " + name + '\n';
            output += "Yearly Pay: $" + yearlyPay.ToString("0.00") + '\n';
            output += "State: " + state + '\n';
            output += "Tax Due: $" + taxDue.ToString("0.00") + '\n';
            return output;
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public decimal YearlyPay
        {
            get { return yearlyPay; }
            set { yearlyPay = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public decimal TaxDue
        {
            get { return taxDue; }
            set { taxDue = value; }
        }
    }

    static class EmployeeList
    {
        public static List<EmployeeRecord> EmployeeRecords;
        //Read in a list of employees from the file
        static EmployeeList()
        {
            Console.WriteLine("Loading Employee Records");
            //Initialize the EmployeeRecords List
            EmployeeRecords = new List<EmployeeRecord>();

            var file = File.OpenText("employees.csv");
            string line;
            int lineIndex = 1;
            while((line = file.ReadLine()) != null)
            {
                string[] data = line.Split(',');
                if(data.Length != 5)
                {
                    Console.WriteLine($"Error Line {lineIndex}: Wrong number of input data given. Gave {data.Length} inputs when it should be 5");
                    lineIndex++;
                    continue;
                }
                //Index 0: Employee ID
                int id;
                if(!int.TryParse(data[0], out id))
                {
                    Console.WriteLine($"Error Line {lineIndex}: Invalid Employee ID");
                    lineIndex++;
                    continue;
                }
                //Index 1: Employee Name
                string name;
                if (data[1] == null)
                {
                    Console.WriteLine($"Error Line {lineIndex}: Empty name input");
                    lineIndex++;
                    continue;
                }
                name = data[1];
                //Index 2: State
                string state;
                if (data[2].Length != 2 || !TaxCalculator.States.ContainsKey(data[2]))
                {
                    Console.WriteLine($"Error Line {lineIndex}: Improper state abreviation: '{data[2]}' when it should be Ex. 'CA', or is not a valid state");
                    lineIndex++;
                    continue;
                }
                state = data[2];
                //Calculate the amount eared for this year
                //Index 3: Number of hours worked
                int hoursWorked;
                if (!int.TryParse(data[3], out hoursWorked))
                {
                    Console.WriteLine($"Error Line {lineIndex}: Invalid format for hours worked");
                    lineIndex++;
                    continue;
                }
                //Index 4: Hourly Rate
                decimal hourlyRate;
                if (!decimal.TryParse(data[4], out hourlyRate))
                {
                    Console.WriteLine($"Error Line {lineIndex}: Invalid format for hourly rate");
                    lineIndex++;
                    continue;
                }
                decimal yearlyPay = decimal.Round(hourlyRate * hoursWorked, 2);
                EmployeeRecords.Add(new EmployeeRecord(id, name, yearlyPay, state));
            }
            file.Close();
            Console.WriteLine("Employee Records File Read\n");
        }
    }

    static class TaxCalculator
    {
        public static bool Verbose = false;
        public static Dictionary<string, State> States;

        static TaxCalculator()
        {
            Console.WriteLine("Loading State Records");
            //Data file
            var file = File.OpenText("taxtable.csv");
            //states will store all the information on file
            Dictionary<string, State> states = new Dictionary<string, State>();
            string line;
            int lineIndex = 1;
            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split(',');

                //data sould be of length 5 
                if (data.Length != 5)
                {
                    Console.WriteLine($"Error Line {lineIndex}: Wrong number of input data given. Gave {data.Length} inputs when it should be 5");
                    lineIndex++;
                    continue;
                }
                //Index 0: State abbreviation; string (2 char)
                if (data[0] == null || data[0].Length != 2)
                {
                    Console.WriteLine($"Error Line {lineIndex}: Improper state abreviation: '{data[0]}' when it should be Ex. 'CA'");
                    lineIndex++;
                    continue;
                }

                //if the state abbreviation checks out, then we can try to add the key to the dictionary
                if (!states.ContainsKey(data[0]))
                {
                    //States did not contain the key, so a new state found in the list
                    State state = new State();
                    //Set the abbreviated name
                    state.AbvrName = data[0];

                    //Index 1: Full State Name; string
                    if (data[1] == null)
                    {
                        Console.WriteLine($"Error Line {lineIndex}: Empty state input");
                        lineIndex++;
                        continue;
                    }
                    state.FullName = data[1];

                    //Since this is a new state, we need a new list for Tax
                    List<Tax> taxes = new List<Tax>();
                    int floor;
                    long ceiling;
                    decimal taxRate;
                    //Index 2: Tax floor; int
                    if (!int.TryParse(data[2], out floor))
                    {
                        Console.WriteLine($"Error Line {lineIndex}: Unable to parse tax floor");
                        lineIndex++;
                        continue;
                    }
                    //Index 3: Tax cieling; int
                    if (!long.TryParse(data[3], out ceiling))
                    {
                        Console.WriteLine($"Error Line {lineIndex}: Unable to parse tax ceiling");
                        lineIndex++;
                        continue;
                    }
                    //Index 4: Tax rate; decimal
                    if (!decimal.TryParse(data[4], out taxRate))
                    {
                        Console.WriteLine($"Error Line {lineIndex}: Unable to parse tax rate");
                        lineIndex++;
                        continue;
                    }
                    //Generate the new Tax object using the temp variables
                    Tax t = new Tax(floor, ceiling, taxRate);
                    //Add the new tax item to the list
                    taxes.Add(t);
                    //Add the list to the state object
                    state.Taxes = taxes;
                    //Add the state to the states dictionary
                    states.Add(data[0], state);
                }
                else
                {
                    //state already in the list
                    //Since the state is already in the list we don't need to instantiate a new state object
                    //and can just modify the existing object in the states dictionary
                    //We still need temp variables to store the data though
                    int floor;
                    long ceiling;
                    decimal taxRate;
                    //Index 2: Tax floor; int
                    if (!int.TryParse(data[2], out floor))
                    {
                        Console.WriteLine($"Error Line {lineIndex}: Unable to parse tax floor");
                        lineIndex++;
                        continue;
                    }
                    //Index 3: Tax cieling; int
                    if (!long.TryParse(data[3], out ceiling))
                    {
                        Console.WriteLine($"Error Line {lineIndex}: Unable to parse tax ceiling");
                        lineIndex++;
                        continue;
                    }
                    //Index 4: Tax rate; decimal
                    if (!decimal.TryParse(data[4], out taxRate))
                    {
                        Console.WriteLine($"Error Line {lineIndex}: Unable to parse tax rate");
                        lineIndex++;
                        continue;
                    }
                    //Generate the new Tax object using the temp variables
                    Tax t = new Tax(floor, ceiling, taxRate);
                    //Add the new tax item to the list
                    states[data[0]].Taxes.Add(t);
                }
                //Increment the line index
                lineIndex++;
            }

            file.Close();
            Console.WriteLine($"States Loaded: {states.Count}");
            States = states;
            Console.WriteLine($"States Count: {States.Count}");
            Console.WriteLine("State Records File Read\n");
        }

        public static decimal ComputeTaxFor(string state, string earned)
        {
            if(States.ContainsKey(state))
            {
                decimal amount;
                if (decimal.TryParse(earned, out amount))
                {
                    decimal taxDue = 0;
                    //Compute the tax
                    //First find the tax bracket they are in using the amount provided
                    //load the tax rates for their state into a temp list
                    List<Tax> taxes = States[state].Taxes;
                    //and keep track of where it is in the list using an counter
                    int index = 0;
                    foreach (Tax t in taxes)
                    {
                        if (amount < t.Ceiling)
                        {
                            //break because the ceiling is higher than the provided amount
                            break;
                        }
                        index++;
                    }
                    //Use amountTaxed to keep track of every dollar out of the amount that has
                    //already been taxed
                    decimal amountTaxed = 0;
                    //Now perform the calculations
                    //First check to see if verbose is on
                    if (Verbose)
                    {
                        Console.WriteLine("\nCalculations Performed\n" + String.Empty.PadLeft(50, '*'));
                        for (int i = index; i > -1; i--)
                        {
                            //Only print if the amount taxed is greater than zero
                            if((amount - taxes[i].Floor - amountTaxed) * taxes[i].TaxRate != 0)
                            {
                                Console.WriteLine($"${amount - taxes[i].Floor - amountTaxed} was taxed at %{taxes[i].TaxRate}: $"
                                + ((amount - taxes[i].Floor - amountTaxed) * taxes[i].TaxRate).ToString("0.00"));
                            }

                            taxDue += (amount - taxes[i].Floor - amountTaxed) * taxes[i].TaxRate;
                            amountTaxed += amount - taxes[i].Floor - amountTaxed;

                        }
                        Console.WriteLine(String.Empty.PadLeft(50, '-'));
                    }
                    else
                    {
                        for (int i = index; i > -1; i--)
                        {
                            taxDue += (amount - taxes[i].Floor - amountTaxed) * taxes[i].TaxRate;
                            amountTaxed += amount - taxes[i].Floor - amountTaxed;
                        }
                    }

                    return taxDue;
                }
                else
                {
                    throw new Exception("Error: enter an decimal or integer value for the amount earned");
                }
            }
            else
            {
                throw new Exception($"Error: {state} was not in the list of states, or contained an error in the data");
            }
        }
    }
}
