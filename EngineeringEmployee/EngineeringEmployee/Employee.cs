using System;

namespace EngineeringEmployee
{
	class Employee
	{
		string _name;
		int _salary;
		string _hireDate;

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public int Salary
		{
			get { return _salary; }
			set { _salary = value; }
		}

		public string HireDate
		{
			get { return _hireDate; }
			set { _hireDate = value; }
		}


		public Employee(string name, int salary, string hireDate)
		{
			Name = name;
			Salary = salary;
			HireDate = hireDate;
		}

		public virtual void GetName()
		{
			Console.WriteLine($"Employee Name: {Name}");
		}

		public virtual void GetSalary()
		{
			Console.WriteLine($"Salary: ${Salary}");
		}

		public virtual void GetHireDate()
		{
			Console.WriteLine($"Hire Date: {HireDate}");
		}

	}

	class Engineer : Employee
	{
		string _schoolAttended;

		public string SchoolAttended
		{
			get { return _schoolAttended; }
			set { _schoolAttended = value; }
		}

		public Engineer(string name, int salary, string hireDate, string schoolAttended) : base(name, salary, hireDate)
		{
			SchoolAttended = schoolAttended;
		}
	}

	class SoftwareEngineer : Engineer
	{

		public SoftwareEngineer(string name, int salary, string hireDate, string schoolAttended) : base(name, salary, hireDate, schoolAttended) { }

		public override void GetSalary()
		{
			Console.WriteLine("Salary: Sorry this information is private");
		}

	}
}
