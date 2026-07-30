using June2026.Database.AppDbContextModels;

namespace June2026.WebApiHttpClientAssignment.Api.Models;

public class SaleCreateRequestModel
{
    public DateTime SaleDate { get; set; }

    public decimal TotalAmount { get; set; }

    public List<SaleDetailCreateRequestModel> SaleDetails { get; set; }

}

public class SaleCreateResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }

    public int SaleId { get; set; }
}