Console.WriteLine("This is a practice 3 of an abstract class in OOP");

CreditCardPayment credit = new CreditCardPayment("Htet", 250);
PayPalPayment paypal = new PayPalPayment("Winter", 400);
PayPalPayment paypal2 = new PayPalPayment("Shwe", -500);   // the amount validation failed case

if (credit.ValidateAmount())
{
    credit.ProcessPayment();
    credit.PrintReceipt();
}
else
{
    throw new Exception("Amount must be greater than $0.");
}

if (paypal.ValidateAmount())
{
    paypal.ProcessPayment();
    paypal.PrintReceipt();
}
else
{
    throw new Exception("Amount must be greater than $0.");
}

if (paypal2.ValidateAmount())
{
    paypal2.ProcessPayment();
    paypal2.PrintReceipt();
}
else
{
    throw new Exception("Amount must be greater than $0.");
}

public abstract class PaymentMethod
{
    public string AccountName { get; set; }

    public decimal Amount { get; set; }

    public PaymentMethod(string accountName, decimal amount)
    {
        AccountName = accountName;
        Amount = amount;
    }

    public abstract void ProcessPayment();

    
    /* the purpose of this virtual method is 
     * for a child to override 
     * if they have a different receipt format to print
     * */
    public virtual void PrintReceipt()
    {
        Console.WriteLine($"Receipt \n Account: {AccountName} \n Amount: ${Amount}");
    }

    public bool ValidateAmount()
    {
        return Amount > 0;
    }
}

class CreditCardPayment : PaymentMethod
{
    public CreditCardPayment(string accountName, decimal amount) : base (accountName, amount) { }

    public override void ProcessPayment()
    {
        Console.WriteLine("The payment is done by Credit Card.");
    }
}

class PayPalPayment: PaymentMethod
{
    public PayPalPayment(string accountName, decimal amount) : base(accountName, amount) { }

    public override void ProcessPayment()
    {
        Console.WriteLine("The payment is done by PayPal.");
    }

    public override void PrintReceipt()
    {
        Console.WriteLine($"PayPal Receipt \n PayPal Account: {AccountName} \n Amount: ${Amount}");
    }
}