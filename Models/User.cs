﻿namespace CinemaManagement.Models;


public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public string? Gender { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
