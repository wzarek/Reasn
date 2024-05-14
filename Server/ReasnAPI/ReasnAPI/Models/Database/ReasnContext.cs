using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<EventParameter> EventParameters { get; set; }

    public virtual DbSet<EventTag> EventTags { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Interest> Interests { get; set; }

    public virtual DbSet<ObjectType> ObjectTypes { get; set; }

    public virtual DbSet<Parameter> Parameters { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInterest> UserInterests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("name=ConnectionStrings:DefaultValue");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("address_pkey");

            entity.ToTable("address", "common");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .HasColumnName("country");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .HasColumnName("state");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .HasColumnName("street");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(255)
                .HasColumnName("zip_code");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comment_pkey");

            entity.ToTable("comment", "events");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
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
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.OrganizerId).HasColumnName("organizer_id");
            entity.Property(e => e.Slug)
                .HasColumnType("character varying")
                .HasColumnName("slug");
            entity.Property(e => e.StartAt).HasColumnName("start_at");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(d => d.Address).WithMany(p => p.Events)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_address_id_fkey");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Events)
                .HasForeignKey(d => d.OrganizerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_organizer_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Events)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_status_id_fkey");
        });

        modelBuilder.Entity<EventParameter>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("event_parameter", "events");

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.ParameterId).HasColumnName("parameter_id");

            entity.HasOne(d => d.Event).WithMany()
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_parameter_event_id_fkey");

            entity.HasOne(d => d.Parameter).WithMany()
                .HasForeignKey(d => d.ParameterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_parameter_parameter_id_fkey");
        });

        modelBuilder.Entity<EventTag>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("event_tag", "events");

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");

            entity.HasOne(d => d.Event).WithMany()
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_tag_event_id_fkey");

            entity.HasOne(d => d.Tag).WithMany()
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_tag_tag_id_fkey");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("image_pkey");

            entity.ToTable("image", "common");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImageData).HasColumnName("image_data");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");
            entity.Property(e => e.ObjectTypeId).HasColumnName("object_type_id");

            entity.HasOne(d => d.ObjectType).WithMany(p => p.Images)
                .HasForeignKey(d => d.ObjectTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("image_object_type_id_fkey");
        });

        modelBuilder.Entity<Interest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("interest_pkey");

            entity.ToTable("interest", "users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ObjectType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("object_type_pkey");

            entity.ToTable("object_type", "common");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Parameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("parameter_pkey");

            entity.ToTable("parameter", "events");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Key)
                .HasMaxLength(255)
                .HasColumnName("key");
            entity.Property(e => e.Value)
                .HasMaxLength(255)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("participant_pkey");

            entity.ToTable("participant", "events");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Event).WithMany(p => p.Participants)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("participant_event_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Participants)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("participant_status_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Participants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("participant_user_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role", "users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_pkey");

            entity.ToTable("status", "common");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ObjectTypeId).HasColumnName("object_type_id");

            entity.HasOne(d => d.ObjectType).WithMany(p => p.Statuses)
                .HasForeignKey(d => d.ObjectTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("status_object_type_id_fkey");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tag_pkey");

            entity.ToTable("tag", "events");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user", "users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Surname)
                .HasMaxLength(255)
                .HasColumnName("surname");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");

            entity.HasOne(d => d.Address).WithMany(p => p.Users)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_address_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_id_fkey");
        });

        modelBuilder.Entity<UserInterest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("user_interest", "users");

            entity.Property(e => e.InterestId).HasColumnName("interest_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Interest).WithMany()
                .HasForeignKey(d => d.InterestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_interest_interest_id_fkey");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_interest_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
