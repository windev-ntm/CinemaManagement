namespace CinemaManagement.Data.Models;

public partial class Invoice
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int TotalPrice { get; set; }

    public DateTime? BoughtTime { get; set; }

    public virtual ICollection<DiscountOnInvoice> DiscountOnInvoices { get; set; } = new List<DiscountOnInvoice>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual User? User { get; set; }
}
