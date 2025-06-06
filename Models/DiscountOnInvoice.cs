﻿using System;
using System.Collections.Generic;

namespace CinemaManagement.Models;

public partial class DiscountOnInvoice
{
    public int InvoiceId { get; set; }

    public string VoucherCode { get; set; } = null!;

    public int Amount { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
