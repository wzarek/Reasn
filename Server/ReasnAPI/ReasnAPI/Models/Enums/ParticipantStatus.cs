using NpgsqlTypes;

namespace ReasnAPI.Models.Enums;

public enum ParticipantStatus
{
    [PgName("Interested")]
    Interested,

    [PgName("Participating")]
    Participating
}