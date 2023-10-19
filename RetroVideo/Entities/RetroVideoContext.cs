using Microsoft.EntityFrameworkCore;

namespace RetroVideo.Entities;

public partial class RetroVideoContext : DbContext
{
    public RetroVideoContext()
    {
    }

    public RetroVideoContext(DbContextOptions<RetroVideoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Klanten> Klantens { get; set; }

    public virtual DbSet<Reservaty> Reservaties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Film>(entity =>
        {
            entity.ToTable("Films");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GenreId).HasColumnName("genreId");
            entity.Property(e => e.Gereserveerd).HasColumnName("gereserveerd");
            entity.Property(e => e.Prijs)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("prijs");
            entity.Property(e => e.Titel)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("titel");
            entity.Property(e => e.Voorraad).HasColumnName("voorraad");

            entity.HasOne(d => d.Genre).WithMany(p => p.Films)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Films_Genres");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genres");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Naam)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("naam");
        });

        modelBuilder.Entity<Klanten>(entity =>
        {
            entity.ToTable("Klanten");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bus)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("bus");
            entity.Property(e => e.Familienaam)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("familienaam");
            entity.Property(e => e.Gemeente)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("gemeente");
            entity.Property(e => e.Huisnummer)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("huisnummer");
            entity.Property(e => e.Postcode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("postcode");
            entity.Property(e => e.Straat)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("straat");
            entity.Property(e => e.Voornaam)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("voornaam");
        });

        modelBuilder.Entity<Reservaty>(entity =>
        {
            entity.HasKey(e => new { e.KlantId, e.FilmId, e.Reservatie });

            entity.ToTable("Reservaties");

            entity.Property(e => e.KlantId).HasColumnName("klantId");
            entity.Property(e => e.FilmId).HasColumnName("filmId");
            entity.Property(e => e.Reservatie)
                .HasColumnType("datetime")
                .HasColumnName("reservatie");

            entity.HasOne(d => d.Film).WithMany(p => p.Reservaties)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservaties_Films");

            entity.HasOne(d => d.Klant).WithMany(p => p.Reservaties)
                .HasForeignKey(d => d.KlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservaties_Klanten");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
