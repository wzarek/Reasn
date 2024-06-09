using NpgsqlTypes;

namespace ReasnAPI.Models.Enums;

public enum EventStatus
{
    [PgName("Completed")]
    Completed,

    [PgName("Ongoing")]
    Ongoing,

    [PgName("Cancelled")]
    Cancelled,

    [PgName("Approved")]
    Approved,

    [PgName("Pending approval")]
    PendingApproval,

    [PgName("Rejected")]
    Rejected
}