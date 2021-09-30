namespace AnimalFacts
{
    public interface IAnimal
    {
        public void Eat();
    }

    public interface IMammal : IAnimal
    {
        public void Speak();
        public void Run();
    }
}
