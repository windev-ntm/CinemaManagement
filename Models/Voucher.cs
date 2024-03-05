using System;
using System.Collections.Generic;

namespace CinemaManagement.Models;

public partial class Voucher
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public int? Amount { get; set; }

    public int? MinSubtotal { get; set; }

    public bool? HasCustomLogic { get; set; }
}
