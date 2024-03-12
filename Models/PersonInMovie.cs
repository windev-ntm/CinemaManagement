namespace CinemaManagement.Models;

public partial class PersonInMovie
{
    public int MovieId { get; set; }

    public int PersonId { get; set; }

    public string? Role { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
