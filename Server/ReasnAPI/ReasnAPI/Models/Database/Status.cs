using System;
using System.Collections.Generic;

namespace ReasnAPI.Models.Database;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ObjectTypeId { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ObjectType ObjectType { get; set; } = null!;

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
}
