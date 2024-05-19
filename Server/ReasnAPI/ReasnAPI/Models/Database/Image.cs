using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.Database;

public partial class Image
{
    public int Id { get; set; }

    public ObjectType ObjectType { get; set; }

    public byte[] ImageData { get; set; } = null!;

    public int ObjectId { get; set; }
}
