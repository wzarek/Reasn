using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.Database;

public partial class ReasnContext : DbContext
{
    public ReasnContext()
    {
    }

    public ReasnContext(DbContextOptions<ReasnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Interest> Interests { get; set; }

    public virtual DbSet<Parameter> Parameters { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInterest> UserInterests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("name=ConnectionStrings:DefaultValue");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("common", "event_status", new[] { "Completed", "In progress", "Approved", "Waiting for approval" })
            .HasPostgresEnum("common", "object_type", new[] { "Event", "User" })
            .HasPostgresEnum("common", "participant_status", new[] { "Interested", "Participating" })
            .HasPostgresEnum("users", "role", new[] { "User", "Organizer", "Admin" });

        modelBuilder
            .Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder
            .Entity<Event>()
            .Property(u => u.Status)
            .HasConversion<string>();

        modelBuilder
            .Entity<Image>()
            .Property(u => u.ObjectType)
            .HasConversion<string> ();

        modelBuilder
            .Entity<Participant>()
            .Property(u => u.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("address_pkey");

            entity.ToTable("address", "common");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Street).HasColumnName("street");
            entity.Property(e => e.ZipCode).HasColumnName("zip_code");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comment_pkey");

            entity.ToTable("comment", "events");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Event).WithMany(p => p.Comments)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_event_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_user_id_fkey");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("event_pkey");

            entity.ToTable("event", "events");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndAt).HasColumnName("end_at");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OrganizerId).HasColumnName("organizer_id");
            entity.Property(e => e.Slug).HasColumnName("slug");
            entity.Property(e => e.StartAt).HasColumnName("start_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Address).WithMany(p => p.Events)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_address_id_fkey");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Events)
                .HasForeignKey(d => d.OrganizerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_organizer_id_fkey");

            entity.HasMany(d => d.Tags).WithMany(p => p.Events)
                .UsingEntity<Dictionary<string, object>>(
                    "EventTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("event_tag_tag_id_fkey"),
                    l => l.HasOne<Event>().WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("event_tag_event_id_fkey"),
                    j =>
                    {
                        j.HasKey("EventId", "TagId").HasName("event_tag_pkey");
                        j.ToTable("event_tag", "events");
                        j.IndexerProperty<int>("EventId").HasColumnName("event_id");
                        j.IndexerProperty<int>("TagId").HasColumnName("tag_id");
                    });
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("image_pkey");

            entity.ToTable("image", "common");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImageData).HasColumnName("image_data");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");
            entity.Property(e => e.ObjectType).HasColumnName("object_type");
        });

        modelBuilder.Entity<Interest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("interest_pkey");

            entity.ToTable("interest", "users");

            entity.HasIndex(e => e.Name, "interest_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Parameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("parameter_pkey");

            entity.ToTable("parameter", "events");

            entity.HasIndex(e => new { e.Key, e.Value }, "parameter_key_value_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasMany(d => d.Events).WithMany(p => p.Parameters)
                .UsingEntity<Dictionary<string, object>>(
                    "EventParameter",
                    r => r.HasOne<Event>().WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("event_parameter_event_id_fkey"),
                    l => l.HasOne<Parameter>().WithMany()
                        .HasForeignKey("ParameterId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("event_parameter_parameter_id_fkey"),
                    j =>
                    {
                        j.HasKey("ParameterId", "EventId").HasName("event_parameter_pkey");
                        j.ToTable("event_parameter", "events");
                        j.IndexerProperty<int>("ParameterId").HasColumnName("parameter_id");
                        j.IndexerProperty<int>("EventId").HasColumnName("event_id");
                    });
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("participant_pkey");

            entity.ToTable("participant", "events");

            entity.HasIndex(e => new { e.EventId, e.UserId }, "participant_event_id_user_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Event).WithMany(p => p.Participants)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("participant_event_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Participants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("participant_user_id_fkey");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tag_pkey");

            entity.ToTable("tag", "events");

            entity.HasIndex(e => e.Name, "tag_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user", "users");

            entity.HasIndex(e => e.Email, "user_email_key").IsUnique();

            entity.HasIndex(e => e.Phone, "user_phone_key").IsUnique();

            entity.HasIndex(e => e.Username, "user_username_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Surname).HasColumnName("surname");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.Address).WithMany(p => p.Users)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_address_id_fkey");
        });

        modelBuilder.Entity<UserInterest>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.InterestId }).HasName("user_interest_pkey");

            entity.ToTable("user_interest", "users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.InterestId).HasColumnName("interest_id");
            entity.Property(e => e.Level).HasColumnName("level");

            entity.HasOne(d => d.Interest).WithMany(p => p.UserInterests)
                .HasForeignKey(d => d.InterestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_interest_interest_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserInterests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_interest_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
