using System;
using System.Collections.Generic;

namespace ReasnAPI.Models.Database;

public partial class EventParameter
{
    public int ParameterId { get; set; }

    public int EventId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Parameter Parameter { get; set; } = null!;
}
