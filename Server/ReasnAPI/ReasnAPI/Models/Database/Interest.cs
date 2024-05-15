using System;
using System.Collections.Generic;

namespace ReasnAPI.Models.Database;

public partial class Interest
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<UserInterest> UserInterests { get; set; } = new List<UserInterest>();
}
