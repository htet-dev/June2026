using System;
using System.Collections.Generic;

namespace June2026.Database.AppDbContextModels;

public partial class TblAddress
{
    public int AddressId { get; set; }

    public string AddressType { get; set; } = null!;

    public string AddressDescription { get; set; } = null!;
}
