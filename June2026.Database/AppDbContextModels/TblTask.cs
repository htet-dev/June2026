using System;
using System.Collections.Generic;

namespace June2026.Database.AppDbContextModels;

public partial class TblTask
{
    public int TaskId { get; set; }

    public string TaskCategory { get; set; } = null!;

    public string TaskDescription { get; set; } = null!;

    public bool IsDelete { get; set; }
}
