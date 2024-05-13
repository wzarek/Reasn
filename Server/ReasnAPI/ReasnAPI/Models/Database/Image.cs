using System;
using System.Collections.Generic;

namespace ReasnAPI.Models.Database;

public partial class Image
{
    public int Id { get; set; }

    public byte[] ImageData { get; set; } = null!;

    public int ObjectTypeId { get; set; }

    public int ObjectId { get; set; }

    public virtual ObjectType ObjectType { get; set; } = null!;
}
