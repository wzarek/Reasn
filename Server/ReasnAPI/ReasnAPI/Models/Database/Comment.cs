using System;
using System.Collections.Generic;

namespace ReasnAPI.Models.Database;

public partial class Comment
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
