using June2026.WebApiHttpClientAssignment.Console;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Nodes;
using static System.Net.Mime.MediaTypeNames;

Start:
Console.WriteLine("Product List: ");
Console.WriteLine("1. View All Products");
Console.WriteLine("2. Create Product");
Console.WriteLine("3. Update Product");
Console.WriteLine("4. Delete Product");
Console.WriteLine("Sale List: ");
Console.WriteLine("5. View All Sales");
Console.WriteLine("6. View Sale by Id");
Console.WriteLine("7. Create Sale");
Console.WriteLine("8. Exit");
int number = 0;

Console.Write("Choose an option: ");
string strNumber = Console.ReadLine()!;
number = Convert.ToInt32(strNumber);

if (number == 1)
{
    // View All Products

    HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.GetAsync("https://localhost:7075/api/product");

    if(response.IsSuccessStatusCode)
    {
        string content = await response.Content.ReadAsStringAsync();

        var products = JsonConvert.DeserializeObject<List<ProductModel>>(content);
        int count = 0;
        foreach(var product in products)
        {
            Console.WriteLine($"{++count}: Product Id: {product.ProductId}, " +
                              $"Product Name: {product.ProductName}, Price: {product.Price}, " +
                              $"Total Available Qty: {product.TotalAvailableQty}");
        }        
    }
}
else if (number == 2)
{
    // Create Product
    Console.Write("Enter Product Name: ");
    string productName = Console.ReadLine()!;
    Console.Write("Enter Description: ");
    string description = Console.ReadLine()!;
    Console.Write("Enter Price: ");
    decimal price = Convert.ToDecimal(Console.ReadLine())!;
    Console.Write("Enter Total Available Qty: ");
    int availableQty = Convert.ToInt32(Console.ReadLine())!;

    ProductCreateRequestModel requestModel = new ProductCreateRequestModel
    {
        ProductName = productName,
        Description = description,
        Price = price,
        TotalAvailableQty = availableQty
    };

    string json = JsonConvert.SerializeObject(requestModel);

    var stringContent = new StringContent(json, Encoding.UTF8, Application.Json);

    HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.PostAsync("https://localhost:7075/api/product", stringContent);

    if(response.IsSuccessStatusCode)
    {
        string content = await response.Content.ReadAsStringAsync();

        var responseModel =  JsonConvert.DeserializeObject<ProductCreateResponseModel>(content);
        Console.WriteLine(responseModel.Message);
    }
}
else if (number == 3)
{
    // Update Product
    Console.Write("Enter Product Id: ");
    string productId = Console.ReadLine()!;

    Console.Write("Enter Product Name: ");
    string productName = Console.ReadLine()!;

    Console.Write("Enter Price: ");
    string? strPrice = Console.ReadLine()!;

    decimal price = 0;

    if(!string.IsNullOrWhiteSpace(strPrice))
    { 
        price = Convert.ToDecimal(strPrice);
    }

    Console.Write("Enter Total Available Qty: ");
    int availableQty = Convert.ToInt32(Console.ReadLine())!;

    ProductPatchRequestModel requestModel = new ProductPatchRequestModel
    {
        ProductName = productName,
        Price = price,
        TotalAvailableQty = availableQty
    };

    string json = JsonConvert.SerializeObject(requestModel);

    var stringContent = new StringContent(json, Encoding.UTF8, Application.Json);

    HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.PatchAsync($"https://localhost:7075/api/product/{productId}", stringContent);

    if (response.IsSuccessStatusCode)
    {
        string content = await response.Content.ReadAsStringAsync();

        var responseModel = JsonConvert.DeserializeObject<ProductPatchResponseModel>(content);
        Console.WriteLine(responseModel.Message);
    }
}
else if(number == 4)
{
    // Delete Product
    Console.Write("Enter Product Id: ");
    string productId = Console.ReadLine()!;

    HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7075/api/product/{productId}");

    if(response.IsSuccessStatusCode)
    {
        string content = await response.Content.ReadAsStringAsync();

        var responseModel = JsonConvert.DeserializeObject<ProductDeleteResponseModel>(content);

        Console.WriteLine(responseModel.Message);        
    }
}
else if (number == 5)
{
    // View All Sales

    HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.GetAsync("https://localhost:7075/api/sale");

    if (response.IsSuccessStatusCode)
    {
        string content = await response.Content.ReadAsStringAsync();

        var sales = JsonConvert.DeserializeObject<List<SaleModel>>(content);

        var groupSales = sales.GroupBy(x => new
        {
            x.SaleId,
            x.TotalAmount
        });

        foreach (var sale in groupSales)
        {
            Console.WriteLine($"Sale Id: {sale.Key.SaleId}");
            Console.WriteLine($"Total Amount: {sale.Key.TotalAmount}");

            int count = 0;

            foreach(var item in sale)
            {
                Console.WriteLine($"{++count}. Product Name: {item.ProductName}, " +
                                  $"Price: {item.UnitPrice}, " + $"Qty: {item.Qty}");
            }            
        }
    }
}
else if (number == 6)
{
    // View Sale by Id

    Console.Write("Enter Sale Id: ");
    string saleId = Console.ReadLine()!;


    HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.GetAsync($"https://localhost:7075/api/sale/{saleId}");

    if (response.IsSuccessStatusCode)
    {
        string content = await response.Content.ReadAsStringAsync();

        var sales = JsonConvert.DeserializeObject<List<SaleModel>>(content);        
                
        Console.WriteLine($"Sale Id: {sales[0].SaleId}");
        Console.WriteLine($"Total Amount: {sales[0].TotalAmount}");

        int count = 0;

        foreach (var item in sales)
        {
            Console.WriteLine($"{++count}. Product Name: {item.ProductName}, " +
                                $"Price: {item.UnitPrice}, " + $"Qty: {item.Qty}");
        }        
    }
}
else if (number == 7)
{
    // Create Sale
    Console.Write("Enter Sale Date (yyyy-MM-dd): ");
    DateTime saleDate = Convert.ToDateTime(Console.ReadLine())!;
    
    Console.Write("Enter Total Amount: ");
    decimal totalAmount = Convert.ToDecimal(Console.ReadLine())!;

    Console.Write("How many products are in this sale? ");
    int count = Convert.ToInt32(Console.ReadLine());

    List<SaleDetailCreateRequestModel> saleDetails = new List<SaleDetailCreateRequestModel>();

    for (int i = 0; i < count; i++)
    {
        Console.Write("Enter Product Id: ");
        int productId = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Price: ");
        decimal price = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Enter Quantity: ");
        int qty = Convert.ToInt32(Console.ReadLine());

        saleDetails.Add(new SaleDetailCreateRequestModel
        {
            ProductId = productId,
            UnitPrice = price,
            Qty = qty
        });
    }

    SaleCreateRequestModel requestModel = new SaleCreateRequestModel
    {
        SaleDate = saleDate,
        TotalAmount = totalAmount,
        saleDetails = saleDetails
    };

    string json = JsonConvert.SerializeObject(requestModel);

    var stringContent = new StringContent(json, Encoding.UTF8, Application.Json);

    HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.PostAsync("https://localhost:7075/api/sale", stringContent);

    if (response.IsSuccessStatusCode)
    {
        string content = await response.Content.ReadAsStringAsync();

        var responseModel = JsonConvert.DeserializeObject<SaleCreateResponseModel>(content);
        Console.WriteLine(responseModel.Message);
    }
}
else
{
    goto Exit;
}

goto Start;

Exit:
Console.WriteLine("Exiting...");
Console.WriteLine("Press any key to continue...");
Console.ReadKey();