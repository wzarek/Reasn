using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Mappers
{
    public static class ImageMapper
    {
        public static ImageDto ToDto(this Image image)
        {
            return new ImageDto
            {
                ImageData = image.ImageData,
                ObjectId = image.ObjectId,
                ObjectType = image.ObjectType
            };
        }

        public static List<ImageDto> ToDtoList(this IEnumerable<Image> images)
        {
            return images.Select(ToDto).ToList();
        }

        public static Image ToEntity(this ImageDto imageDto)
        {
            return new Image
            {
                ImageData = imageDto.ImageData,
                ObjectId = imageDto.ObjectId,
                ObjectType = imageDto.ObjectType
            };
        }

    }
}
