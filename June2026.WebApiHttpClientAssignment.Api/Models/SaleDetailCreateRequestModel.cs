namespace June2026.WebApiHttpClientAssignment.Api.Models;

public class SaleDetailCreateRequestModel
{
    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Qty { get; set; }
}
