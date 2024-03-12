using System;
using System.Collections.Generic;

namespace CinemaManagement.Data.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public int? InvoiceId { get; set; }

    public int? ScreeningId { get; set; }

    public int Seat { get; set; }

    public int Price { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual Screening? Screening { get; set; }
}
