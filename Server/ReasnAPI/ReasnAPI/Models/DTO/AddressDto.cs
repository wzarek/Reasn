using System;

namespace ReasnAPI.Models.DTO {
    public class AddressDto {
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string State { get; set; } = null!;
        public string? ZipCode { get; set; }
    }
}