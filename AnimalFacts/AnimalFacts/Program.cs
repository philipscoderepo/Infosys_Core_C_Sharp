using System;
using System.Collections.Generic;

namespace AnimalFacts
{
    class Program
    {
        static void Main(string[] args)
        {
            Dog dog = new Dog();
            Cat cat = new Cat();
            Cow cow = new Cow();
            Bacterium bacterium = new Bacterium();

            List<IAnimal> animals = new List<IAnimal>();
            animals.Add(dog);
            animals.Add(cat);
            animals.Add(cow);
            animals.Add(bacterium);

            foreach(IAnimal a in animals)
            {
                if(a is IMammal)
                {
                    IMammal m = (IMammal)a;
                    m.Speak();
                    m.Run();
                }
                a.Eat();
            }
        }
    }
}
