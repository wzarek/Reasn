using NpgsqlTypes;

namespace ReasnAPI.Models.Enums;

public enum UserRole
{
    [PgName("User")]
    User,

    [PgName("Organizer")]
    Organizer,

    [PgName("Admin")]
    Admin
}