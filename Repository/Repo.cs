using CinemaManagement.Models;
using Microsoft.EntityFrameworkCore;


public partial class Repo : DbContext
{
    public Repo()
    {

    }
    public Repo(DbContextOptions<Repo> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; } = null;
    public DbSet<BlockbusterMovie> Movies { get; set; } = null;
    public DbSet<DiscountOnInvoice> Discounts { get; set; } = null;
    public DbSet<Invoice> Invoices { get; set; } = null;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=ep-solitary-bar-a1tpb2rn.ap-southeast-1.aws.neon.tech;Database=cinema_management;User Id=admin;Password=age8cutxfXz5;SSL Mode=Require;");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Gender).HasColumnName("gender");
        }
        );

        modelBuilder.Entity<BlockbusterMovie>(entity =>
        {
            entity.ToTable("blockbuster_movie");
            entity.HasKey(e => e.MovieId);
            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.HasOne(d => d.Movie)
                .WithOne(p => p.BlockbusterMovie)
                .HasForeignKey<BlockbusterMovie>(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blockbuster_movie_movie_id_fkey");
        }
        );

        modelBuilder.Entity<DiscountOnInvoice>(entity =>
        {
            entity.ToTable("discount_on_invoice");
            entity.HasKey(e => new { e.InvoiceId, e.VoucherCode });
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.VoucherCode).HasColumnName("voucher_code");
            entity.Property(e => e.Amount).HasColumnName("amount");
        }
               );
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
