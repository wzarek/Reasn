using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.DTOs
{
    public class ImageDto
    {
        public byte[] ImageData { get; set; } = null!;
        public int ObjectId { get; set; }
        public ObjectType ObjectType { get; set; }
    }
}