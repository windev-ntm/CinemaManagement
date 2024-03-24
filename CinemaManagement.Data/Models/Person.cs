namespace CinemaManagement.Data.Models;

public partial class Person
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Avatar { get; set; }

    public string? Bio { get; set; }

    public virtual ICollection<PersonInMovie> PersonInMovies { get; set; } = new List<PersonInMovie>();
}
