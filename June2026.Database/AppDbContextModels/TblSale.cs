using System;
using System.Collections.Generic;

namespace June2026.Database.AppDbContextModels;

public partial class TblSale
{
    public int SaleId { get; set; }

    public DateTime SaleDate { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual ICollection<TblSaleDetail> TblSaleDetails { get; set; } = new List<TblSaleDetail>();
}
