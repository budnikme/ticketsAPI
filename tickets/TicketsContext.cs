using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using tickets.Models.Entities;

namespace tickets
{
    public partial class TicketsContext : DbContext
    {
        public TicketsContext()
        {
        }

        public TicketsContext(DbContextOptions<TicketsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<GetEvent> GetEvents { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Place> Places { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<TicketType> TicketTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.1.100;Database=tickets;User Id=SA;Password=iLbbdv8cca; Encrypt=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("artists", "tickets");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasMany(d => d.Events)
                    .WithMany(p => p.Artists)
                    .UsingEntity<Dictionary<string, object>>(
                        "EventArtist",
                        l => l.HasOne<Event>().WithMany().HasForeignKey("EventId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("eventArtists_events_id_fk"),
                        r => r.HasOne<Artist>().WithMany().HasForeignKey("ArtistId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("eventArtists_artists_id_fk"),
                        j =>
                        {
                            j.HasKey("ArtistId", "EventId").HasName("eventArtists_pk");

                            j.ToTable("eventArtists", "tickets");

                            j.IndexerProperty<int>("ArtistId").HasColumnName("artist_id");

                            j.IndexerProperty<int>("EventId").HasColumnName("event_id");
                        });
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events", "tickets");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.PlaceId).HasColumnName("place_id");

                entity.Property(e => e.PosterLink)
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasColumnName("poster_link");

                entity.Property(e => e.PreviewLink)
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasColumnName("preview_link");

                entity.Property(e => e.Tittle)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tittle");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.PlaceId)
                    .HasConstraintName("events_places_id_fk");

                entity.HasMany(d => d.Genres)
                    .WithMany(p => p.Events)
                    .UsingEntity<Dictionary<string, object>>(
                        "EventGenre",
                        l => l.HasOne<Genre>().WithMany().HasForeignKey("GenreId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("eventGenres_genres_id_fk"),
                        r => r.HasOne<Event>().WithMany().HasForeignKey("EventId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("eventGenres_events_id_fk"),
                        j =>
                        {
                            j.HasKey("EventId", "GenreId").HasName("eventGenres_pk");

                            j.ToTable("eventGenres", "tickets");

                            j.IndexerProperty<int>("EventId").HasColumnName("event_id");

                            j.IndexerProperty<int>("GenreId").HasColumnName("genre_id");
                        });
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genres", "tickets");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Genre1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("genre");
            });

            modelBuilder.Entity<GetEvent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("getEvents");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Place)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PosterLink)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.PreviewLink)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Tittle)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payments", "tickets");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Confirmed)
                    .HasColumnName("confirmed")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("payments_events_id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("payments_users_id_fk");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("places", "tickets");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Tittle)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tittle");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("tickets", "tickets");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("tickets_events_id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("tickets_users_id_fk");
            });

            modelBuilder.Entity<TicketType>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.EventId })
                    .HasName("ticketTypes_pk");

                entity.ToTable("ticketTypes", "tickets");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Tittle)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tittle");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.TicketTypes)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ticketTypes_events_id_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users", "tickets");

                entity.HasIndex(e => e.Email, "users_email_pk")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(512)
                    .HasColumnName("password_hash");

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(512)
                    .HasColumnName("password_salt");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Type)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
