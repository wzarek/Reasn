<<<<<<< HEAD
using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.DTOs
{
    public class ImageDto
=======
using System;

namespace ReasnAPI.Models.DTOs
{
    public class ImageDto
>>>>>>> f457d1a (ci: add pre-commit hook to run fmt and lint)
    {
        public byte[] ImageData { get; set; } = null!;
        public int ObjectId { get; set; }
        public ObjectType ObjectType { get; set; }
    }
}