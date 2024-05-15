using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.Database;

public partial class Participant
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public ParticipantStatus status { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
