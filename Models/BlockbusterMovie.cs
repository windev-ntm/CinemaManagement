﻿namespace CinemaManagement.Models;

public partial class BlockbusterMovie
{
    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
