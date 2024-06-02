using NpgsqlTypes;

namespace ReasnAPI.Models.Enums;

public enum EventStatus
{
    [PgName("Completed")]
    Completed,
    [PgName("InProgress")]
    Inprogress,
    [PgName("Approved")]
    Approved,
    [PgName("WaitingForApproval")]
    WaitingForApproval
}