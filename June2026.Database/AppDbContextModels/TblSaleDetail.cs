using System;
using System.Collections.Generic;

namespace June2026.Database.AppDbContextModels;

public partial class TblSaleDetail
{
    public int SaleDetailId { get; set; }

    public int SaleId { get; set; }

    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Qty { get; set; }

    public virtual TblProduct Product { get; set; } = null!;

    public virtual TblSale Sale { get; set; } = null!;
}
