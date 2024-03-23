using System;

namespace ReasnAPI.Models.DTO {
    public class UserDto {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public AddressDto Address { get; set; }
    }
}