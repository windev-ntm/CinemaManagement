namespace CinemaManagement.Data.Models;

public partial class MovieCertification
{
    public int Id { get; set; }

    public int? AgeLimit { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
