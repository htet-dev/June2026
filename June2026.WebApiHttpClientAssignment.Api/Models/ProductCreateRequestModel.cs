namespace June2026.WebApiHttpClientAssignment.Api.Models;

public class ProductCreateRequestModel
{
    public string ProductName { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public int TotalAvailableQty { get; set; }
}

public class ProductCreateResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public int ProductId { get; set; }

}