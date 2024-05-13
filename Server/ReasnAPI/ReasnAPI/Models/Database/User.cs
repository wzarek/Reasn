using System;
using System.Collections.Generic;

namespace ReasnAPI.Models.Database;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int RoleId { get; set; }

    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    public int AddressId { get; set; }

    public string? Phone { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserInterest> UserInterests { get; set; } = new List<UserInterest>();
}
