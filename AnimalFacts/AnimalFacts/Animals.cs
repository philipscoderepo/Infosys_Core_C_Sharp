using System;

namespace AnimalFacts
{
    class Dog : IMammal
    {
        public void Speak() { Console.WriteLine("Bark!"); }
        public void Run() { Console.WriteLine("Dogs can run at a top speed of 45 mph!"); }
        public void Eat() { Console.WriteLine("Dogs eat bones."); }
    }

    class Cat : IMammal
    {
        public void Speak() { Console.WriteLine("Meow!"); }
        public void Run() { Console.WriteLine("Cats can run at a top speed of 30 mph!"); }
        public void Eat() { Console.WriteLine("Cats eat mice."); }
    }

    class Cow : IMammal
    {
        public void Speak() { Console.WriteLine("Moo!"); }
        public void Run() { Console.WriteLine("Cows can run at a top speed of 25 mph!"); }
        public void Eat() { Console.WriteLine("Cows eat grass."); }
    }

    class Bacterium : IAnimal
    {
        public void Eat() { Console.WriteLine("Bacteria eat viruses."); }
    }
}
