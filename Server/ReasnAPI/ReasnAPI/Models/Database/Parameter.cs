using System;
using System.Collections.Generic;

namespace ReasnAPI.Models.Database;

public partial class Parameter
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
