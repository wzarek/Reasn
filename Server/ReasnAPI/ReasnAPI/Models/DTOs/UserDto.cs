using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.DTOs {
    public class UserDto {
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public UserRole Role { get; set; }
        public int AddressId { get; set; }
        public List<InterestDto>? Interests { get; set; }
    }
}