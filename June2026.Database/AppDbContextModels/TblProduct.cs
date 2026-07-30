using System;
using System.Collections.Generic;

namespace June2026.Database.AppDbContextModels;

public partial class TblProduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public int TotalAvailableQty { get; set; }

    public virtual ICollection<TblSaleDetail> TblSaleDetails { get; set; } = new List<TblSaleDetail>();
}
