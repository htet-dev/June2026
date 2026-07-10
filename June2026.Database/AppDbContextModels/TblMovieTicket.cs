using System;
using System.Collections.Generic;

namespace June2026.Database.AppDbContextModels;

public partial class TblMovieTicket
{
    public int TicketId { get; set; }

    public string TicketType { get; set; } = null!;

    public string TicketName { get; set; } = null!;

    public decimal TicketPrice { get; set; }
}
