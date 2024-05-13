<<<<<<< HEAD
namespace ReasnAPI.Models.DTOs
{
    public class AddressDto
=======
using System;

namespace ReasnAPI.Models.DTOs
{
    public class AddressDto
>>>>>>> f457d1a (ci: add pre-commit hook to run fmt and lint)
    {
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string State { get; set; } = null!;
        public string? ZipCode { get; set; }
    }
}