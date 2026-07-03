Console.WriteLine("This is the wallet transfer program");

TransferService service = new TransferService();
string message = service.WalletTransfer("0950210334", "0985858585", 1000);
Console.WriteLine(message);

message = service.WalletTransfer("0985858585", "0950210334", 500);
Console.WriteLine(message);

public class TransferService
{
    private string message;

    public string WalletTransfer(string fromMobileNo, string toMobileNo, decimal amount = 1000)
    {
        Console.WriteLine($"From Mobile No: {fromMobileNo}");
        Console.WriteLine($"To Mobile No: {toMobileNo}");
        Console.WriteLine($"Amount: {amount}");

        Account fromAccount = Database.Accounts.Where(x => x.MobileNo == fromMobileNo).FirstOrDefault();
        Account toAccount = Database.Accounts.Where(x => x.MobileNo == toMobileNo).FirstOrDefault();

        fromAccount.Balance -= amount;
        toAccount.Balance += amount;
        Console.WriteLine("================================================");

        return "Wallet transfer successful.";    

    }

}


public class Account
{
    public string Name;

    public string MobileNo;

    private decimal balance;
    public decimal Balance 
    { 
        get
        {
            return balance;
        }
        set
        {
            Console.WriteLine($"Mobile No {MobileNo} , Old Value: {balance}");

            if (value < 0)
                throw new Exception("Balance cannot be negative.");

            balance = value;
            Console.WriteLine($"Mobile No {MobileNo} , New Value: {balance}");
        }
    
    }

    public Account()
    { }

    public Account(string mobileNo, decimal balance = 10000)
    {
        MobileNo = mobileNo;
        Balance = balance;
    }

    public Account(string name, string mobileNo, decimal balance = 10000)
    {
        Name = name;
        MobileNo = mobileNo;
        Balance = balance;
    }
}

public static class Database
{
    public static readonly Account[] Accounts = new Account[2]
    {
        new Account()
        {            
            MobileNo = "0950210334",
            Balance = 10000
        },
        new Account()
        {            
            MobileNo = "0985858585",
            Balance = 10000
        }
    };
}