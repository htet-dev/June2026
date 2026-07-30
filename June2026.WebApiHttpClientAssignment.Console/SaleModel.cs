using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace June2026.WebApiHttpClientAssignment.Console;

public class SaleModel
{
    public int SaleId { get; set; }
    public int SaleDetailId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Qty {  get; set; }
    public decimal TotalAmount { get; set; }
}

public class SaleCreateRequestModel
{
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<SaleDetailCreateRequestModel> saleDetails { get; set; }

}

public class SaleCreateResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public int SaleId { get; set; }
}

public class SaleDetailCreateRequestModel
{
    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Qty { get; set; }
}