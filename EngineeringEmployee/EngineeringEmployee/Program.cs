using System;

namespace EngineeringEmployee
{
    class Program
    {
        static void Main(string[] args)
        {
            Engineer e = new Engineer("Rupert Scott", 42000, "11/22/12", "Sacramento University");
            e.GetName();
            e.GetSalary();
            e.GetHireDate();

            Console.WriteLine(String.Empty.PadLeft(50, '-'));

            SoftwareEngineer se = new SoftwareEngineer("Shea Rovington", 42000, "03/27/18", "Harvard University");
            se.GetName();
            se.GetSalary();
            se.GetHireDate();
        }
    }
}
