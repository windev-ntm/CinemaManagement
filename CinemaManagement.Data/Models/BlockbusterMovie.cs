using System;
using System.Collections.Generic;

namespace CinemaManagement.Data.Models;

public partial class BlockbusterMovie
{
    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
