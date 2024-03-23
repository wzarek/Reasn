using System;

namespace ReasnAPI.Models.DTO {
    public class ImageDto { 
        public byte[] ImageData { get; set; }
        public int ObjectId { get; set; }
        public int ObjectTypeId { get; set; }
    }
}