using System;
using System.Collections.Generic;

namespace CinemaManagement.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public TimeSpan? Duration { get; set; }

    public int? PublishedYear { get; set; }

    public float? ImdbRating { get; set; }

    public string? PosterImg { get; set; }

    public int? CertificationId { get; set; }

    public string? Trailer { get; set; }

    public virtual BlockbusterMovie? BlockbusterMovie { get; set; }

    public virtual MovieCertification? Certification { get; set; }

    public virtual ICollection<PersonInMovie> PersonInMovies { get; set; } = new List<PersonInMovie>();

    public virtual ICollection<Screening> Screenings { get; set; } = new List<Screening>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public string? CertificationName { get; internal set; }
}
