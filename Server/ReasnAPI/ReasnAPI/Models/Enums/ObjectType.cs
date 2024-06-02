using NpgsqlTypes;

namespace ReasnAPI.Models.Enums;

public enum ObjectType
{
    [PgName("Event")]
    Event,
    [PgName("User")]
    User
}