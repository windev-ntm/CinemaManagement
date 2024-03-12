using System;
using System.Collections.Generic;

namespace CinemaManagement.Data.Models;

public partial class Screen
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Seats { get; set; }

    public virtual ICollection<Screening> Screenings { get; set; } = new List<Screening>();
}
