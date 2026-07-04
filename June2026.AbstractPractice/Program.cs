Console.WriteLine("This is a practice of an abstract class in OOP");

Dog d = new Dog("Buddy", 3);
Cat c = new Cat("Kitty", 2);
Console.WriteLine("This is how a Dog sounds and sleeps...");
d.MakeSound();
d.Sleep();
Console.WriteLine("=====================================");
Console.WriteLine("This is how a Cat sounds and sleeps...");
c.MakeSound();
c.Sleep();
abstract class Animal
{
    public string Name { get; set; }

    public int Age { get; set; }

    public Animal(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public abstract void MakeSound();

    public void Sleep()
    {
        Console.WriteLine($"{Name} is sleeping.");
    }
}

class Dog : Animal
{
    public Dog(string name, int age) : base(name, age)
    {

    }
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} says Woof!");
    }
}

class Cat : Animal
{
    public Cat(string name, int age) : base (name, age)
    {

    }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} says Meow!");
    }
}
