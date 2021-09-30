using System;

namespace ZooManager
{
	public class Animal
	{
		public string Name
		{
			get; set;
		}

		public Animal()
		{
			Name = "Default";
			Console.WriteLine("Create a default animal");
		}

		public Animal(string name)
		{
			Name = name;
		}
	}
}
