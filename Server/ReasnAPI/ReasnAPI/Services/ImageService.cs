using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;
using Image = ReasnAPI.Models.Database.Image;

namespace ReasnAPI.Services;
public class ImageService(ReasnContext context)
{
    public List<ImageDto> CreateImages(List<ImageDto> imageDtos)
    {
        var newImages = new List<Image>();

        foreach (var imageDto in imageDtos)
        {
            var image = context.Images.FirstOrDefault(r => r.ObjectId == imageDto.ObjectId && r.ObjectTypeId == imageDto.ObjectTypeId);
            if (image is not null)
            {
                continue;
            }

            var newImage = new Image
            {
                ImageData = imageDto.ImageData,
                ObjectId = imageDto.ObjectId,
                ObjectTypeId = imageDto.ObjectTypeId
            };

            newImages.Add(newImage);
        }

        if (newImages.Any())
        {
            var objectType = context.ObjectTypes.FirstOrDefault(ot => ot.Id == newImages.First().ObjectTypeId);
            if (objectType is not null)
            {
                if (objectType.Name == "User" && newImages.Count > 1)
                {
                    context.Images.Add(newImages.First());
                }
                else if (objectType.Name == "Event")
                {
                    context.Images.AddRange(newImages);
                }
            }

            context.SaveChanges();
        }

        return imageDtos;
    }

    public ImageDto? UpdateImage(int imageId, ImageDto imageDto)
    {
        var image = context.Images.FirstOrDefault(r => r.Id == imageId);
        if (image is null)
        {
            return null;
        }

        image.ObjectId = imageDto.ObjectId;
        image.ImageData = imageDto.ImageData;
        image.ObjectTypeId = imageDto.ObjectTypeId;

        context.Images.Update(image);
        context.SaveChanges();
        return imageDto;
    }

    public bool DeleteImage(int id)
    {
        var image = context.Images.FirstOrDefault(r => r.Id == id);
        if (image is null)
        {
            return false;
        }
        context.Images.Remove(image);
        context.SaveChanges();

        return true;
    }

    public ImageDto? GetImageById(int id)
    {
        var image = context.Images.Find(id);
        if (image is null)
        {
            return null;
        }

        var imageDto = new ImageDto
        {
            ImageData = image.ImageData,
            ObjectId = image.ObjectId,
            ObjectTypeId = image.ObjectTypeId
        };

        return imageDto;
    }

    public IEnumerable<ImageDto> GetAllImages()
    {
        var images = context.Images.ToList();

        var imageDtos = images.Select(image => new ImageDto
        {
            ImageData = image.ImageData,
            ObjectId = image.ObjectId,
            ObjectTypeId = image.ObjectTypeId
        }).ToList();

        return imageDtos;
    }

    public IEnumerable<ImageDto> GetImagesByFilter(Expression<Func<Image, bool>> filter)
    {
        var images = context.Images.Where(filter).ToList();

        var imageDtos = images.Select(image => new ImageDto
        {
            ImageData = image.ImageData,
            ObjectId = image.ObjectId,
            ObjectTypeId = image.ObjectTypeId
        }).AsEnumerable();

        return imageDtos;
    }

    public IEnumerable<ImageDto> GetImagesByEventIdAndType(int eventId)
    {
        var images = context.Images
            .Where(image => image.ObjectType.Name == "event" && image.ObjectId == eventId)
            .ToList();

        var imageDtos = images.Select(image => new ImageDto
        {
            ImageData = image.ImageData,
            ObjectId = image.ObjectId,
            ObjectTypeId = image.ObjectTypeId
        }).AsEnumerable();

        return imageDtos;
    }

}