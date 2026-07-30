using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace June2026.WebApiHttpClientAssignment.Console;

public class ProductModel
{
    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int TotalAvailableQty { get; set; }

}

public class ProductCreateRequestModel
{
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int TotalAvailableQty { get; set; }
}

public class ProductCreateResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public int ProductId { get; set; }
}

public class ProductPatchRequestModel
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int TotalAvailableQty { get; set; }
}

public class ProductPatchResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}

public class ProductDeleteResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}