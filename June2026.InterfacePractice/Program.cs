Console.WriteLine("Choose Payment Type by typing 1, 2 or 3...");
Console.WriteLine("1: Credit Card");
Console.WriteLine("2: Pay Pal");
Console.WriteLine("3: Apple Pay");

int input = Convert.ToInt16(Console.ReadLine());

if (!Enum.IsDefined(typeof(PaymentType), input))
{
    Console.WriteLine("Invalid payment type.");
    return;
}

PaymentType paymentType = (PaymentType)input;

IPaymentService service;

Console.ReadLine();

switch (paymentType)
{
    case PaymentType.CreditCard:
        service = new CreditCardPaymentService();
        break;
    case PaymentType.PayPal:
        service = new PayPalPaymentService();
        break;
    case PaymentType.ApplePay:
        service = new ApplePayPaymentService();
        break;
    case 0:
    default:
        throw new Exception("Invalid Payment Type");
}

Console.WriteLine(service.MakePayment());

public interface IPaymentService
{
    string MakePayment();
}

public class CreditCardPaymentService : IPaymentService
{
    public string MakePayment()
    {
        return "Credit Card Payment is done";
    }
}

public class PayPalPaymentService : IPaymentService
{
    public string MakePayment()
    {
        return "Pay Pal Payment is done";
    }
}

public class ApplePayPaymentService : IPaymentService
{
    public string MakePayment()
    {
        return "Apple Pay Payment is done";
    }
}

public enum PaymentType
{
    None,
    CreditCard,
    PayPal,
    ApplePay,
}