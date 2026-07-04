Console.WriteLine("This is a practice 2 of an abstract class in OOP");

Manager m = new Manager("Alice", 5000);
Developer d = new Developer("Bob", 4000);
Console.WriteLine("Manager");
m.DisplayInfo();
Console.WriteLine("====================================");
Console.WriteLine("Developer");
d.DisplayInfo();
public abstract class Employee
{
    public string Name { get; set; }
    public decimal Salary { get; set; }

    public Employee(string name, decimal salary)
    {
        Name = name;
        Salary = salary;
    }

    public abstract decimal CalculateBonus();

    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name} \n Salary: {Salary} \n Bonus: {CalculateBonus()}");
    }
}

class Manager : Employee
{
    public Manager(string name, decimal salary) : base(name, salary) { }

    public override decimal CalculateBonus()
    {
        return (Salary * 20) / 100;
    }
}

class Developer : Employee
{
    public Developer(string name, decimal salary) : base(name, salary) { }

    public override decimal CalculateBonus()
    {
        return (Salary * 10) / 100;
    }
}