using System;
using System.Collections.Generic;

namespace ReasnAPI.Models.Database;

public partial class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
