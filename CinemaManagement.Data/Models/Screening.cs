namespace CinemaManagement.Data.Models;

public partial class Screening
{
    public int Id { get; set; }

    public int? MovieId { get; set; }

    public int? ScreenId { get; set; }

    public DateTime? StartTime { get; set; }

    public int? Price { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual Screen? Screen { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
