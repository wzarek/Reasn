using System;
using System.Collections.Generic;

namespace ReasnAPI.Models.Database;

public partial class ObjectType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();
}
