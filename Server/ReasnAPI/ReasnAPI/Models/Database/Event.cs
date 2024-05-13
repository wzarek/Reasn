using System;
using System.Collections.Generic;

namespace ReasnAPI.Models.Database;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int AddressId { get; set; }

    public string Description { get; set; } = null!;

    public int OrganizerId { get; set; }

    public DateTime StartAt { get; set; }

    public DateTime EndAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Slug { get; set; } = null!;

    public int StatusId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User Organizer { get; set; } = null!;

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<Parameter> Parameters { get; set; } = new List<Parameter>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
