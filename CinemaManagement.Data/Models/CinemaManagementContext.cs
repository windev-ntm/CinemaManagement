using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Reflection;

namespace CinemaManagement.Data.Models;

public partial class CinemaManagementContext : DbContext
{
    public CinemaManagementContext()
    {
    }

    public CinemaManagementContext(DbContextOptions<CinemaManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<BlockbusterMovie> BlockbusterMovies { get; set; }

    public virtual DbSet<DiscountOnInvoice> DiscountOnInvoices { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieCertification> MovieCertifications { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonInMovie> PersonInMovies { get; set; }

    public virtual DbSet<Screen> Screens { get; set; }

    public virtual DbSet<Screening> Screenings { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: Secure the connection string
        // But is this really necessary since we are connecting directly to the database anw?
        var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
        optionsBuilder.UseNpgsql(config.ConnectionStrings.ConnectionStrings["CinemaManagementDatabase"].ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("admin_pkey");

            entity.ToTable("admin");

            entity.HasIndex(e => e.Username, "admin_username_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        modelBuilder.Entity<BlockbusterMovie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("blockbuster_movie_pkey");

            entity.ToTable("blockbuster_movie");

            entity.Property(e => e.MovieId)
                .ValueGeneratedNever()
                .HasColumnName("movie_id");

            entity.HasOne(d => d.Movie).WithOne(p => p.BlockbusterMovie)
                .HasForeignKey<BlockbusterMovie>(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blockbuster_movie_movie_id_fkey");
        });

        modelBuilder.Entity<DiscountOnInvoice>(entity =>
        {
            entity.HasKey(e => new { e.InvoiceId, e.VoucherCode }).HasName("discount_on_invoice_pkey");

            entity.ToTable("discount_on_invoice");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.VoucherCode).HasColumnName("voucher_code");
            entity.Property(e => e.Amount).HasColumnName("amount");

            entity.HasOne(d => d.Invoice).WithMany(p => p.DiscountOnInvoices)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("discount_on_invoice_invoice_id_fkey");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genre_pkey");

            entity.ToTable("genre");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasMany(d => d.Movies).WithMany(p => p.Genres)
                .UsingEntity<Dictionary<string, object>>(
                    "GenreOfMovie",
                    r => r.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("genre_of_movie_movie_id_fkey"),
                    l => l.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("genre_of_movie_genre_id_fkey"),
                    j =>
                    {
                        j.HasKey("GenreId", "MovieId").HasName("genre_of_movie_pkey");
                        j.ToTable("genre_of_movie");
                        j.IndexerProperty<int>("GenreId").HasColumnName("genre_id");
                        j.IndexerProperty<int>("MovieId").HasColumnName("movie_id");
                    });
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invoice_pkey");

            entity.ToTable("invoice");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BoughtTime).HasColumnName("bought_time");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("invoice_user_id_fkey");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("movie_pkey");

            entity.ToTable("movie");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CertificationId).HasColumnName("certification_id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.ImdbRating).HasColumnName("imdb_rating");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PosterImg).HasColumnName("poster_img");
            entity.Property(e => e.PublishedYear).HasColumnName("published_year");
            entity.Property(e => e.Trailer).HasColumnName("trailer");

            entity.HasOne(d => d.Certification).WithMany(p => p.Movies)
                .HasForeignKey(d => d.CertificationId)
                .HasConstraintName("movie_certification_id_fkey");
        });

        modelBuilder.Entity<MovieCertification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("movie_certification_pkey");

            entity.ToTable("movie_certification");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgeLimit).HasColumnName("age_limit");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("person_pkey");

            entity.ToTable("person");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Avatar).HasColumnName("avatar");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<PersonInMovie>(entity =>
        {
            entity.HasKey(e => new { e.MovieId, e.PersonId }).HasName("person_in_movie_pkey");

            entity.ToTable("person_in_movie");

            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.Movie).WithMany(p => p.PersonInMovies)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_in_movie_movie_id_fkey");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonInMovies)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_in_movie_person_id_fkey");
        });

        modelBuilder.Entity<Screen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("screen_pkey");

            entity.ToTable("screen");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Seats).HasColumnName("seats");
        });

        modelBuilder.Entity<Screening>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("screening_pkey");

            entity.ToTable("screening");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ScreenId).HasColumnName("screen_id");
            entity.Property(e => e.StartTime).HasColumnName("start_time");

            entity.HasOne(d => d.Movie).WithMany(p => p.Screenings)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("screening_movie_id_fkey");

            entity.HasOne(d => d.Screen).WithMany(p => p.Screenings)
                .HasForeignKey(d => d.ScreenId)
                .HasConstraintName("screening_screen_id_fkey");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ticket_pkey");

            entity.ToTable("ticket");

            entity.HasIndex(e => new { e.ScreeningId, e.Seat }, "ticket_screening_id_seat_idx").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ScreeningId).HasColumnName("screening_id");
            entity.Property(e => e.Seat).HasColumnName("seat");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("ticket_invoice_id_fkey");

            entity.HasOne(d => d.Screening).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ScreeningId)
                .HasConstraintName("ticket_screening_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.Username, "user_username_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("voucher_pkey");

            entity.ToTable("voucher");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.HasCustomLogic)
                .HasDefaultValue(false)
                .HasColumnName("has_custom_logic");
            entity.Property(e => e.MinSubtotal).HasColumnName("min_subtotal");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
