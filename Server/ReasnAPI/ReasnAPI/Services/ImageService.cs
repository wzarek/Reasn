using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using ReasnAPI.Models.Enums;
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
            var image = context.Images.FirstOrDefault(r => r.ObjectId == imageDto.ObjectId && r.ObjectType == imageDto.ObjectType);
            if (image is not null)
            {
                continue;
            }

            var newImage = MapImageFromImageDto(imageDto);
            
            newImages.Add(newImage);
        }

        if (newImages.Any())
        {
            var objectType = newImages.First().ObjectType;
          
        
            if (objectType == ObjectType.User && newImages.Count == 1)
            {
                context.Images.Add(newImages.First());
            }
            else if (objectType == ObjectType.Event)
            {
                context.Images.AddRange(newImages);
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
        image.ObjectType = imageDto.ObjectType;

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

        var imageDto = MapImageDtoFromImage(image);
        
        return imageDto;
    }

    public IEnumerable<ImageDto> GetAllImages()
    {
        var images = context.Images.ToList();

        var imageDtos = images.Select(image => MapImageDtoFromImage(image)).AsEnumerable();

        return imageDtos;
    }

    public IEnumerable<ImageDto> GetImagesByFilter(Expression<Func<Image, bool>> filter)
    {
        var images = context.Images.Where(filter).ToList();

        var imageDtos = images.Select(image => MapImageDtoFromImage(image)).AsEnumerable();

        return imageDtos;
    }

    public IEnumerable<ImageDto> GetImagesByEventIdAndType(int eventId)
    {
        var images = context.Images
            .Where(image => image.ObjectType == ObjectType.Event && image.ObjectId == eventId)
            .ToList();

        var imageDtos = images.Select(image => MapImageDtoFromImage(image)).AsEnumerable();

        return imageDtos;
    }

    private ImageDto MapImageDtoFromImage(Image image)
    {
        return new ImageDto
        {
            ImageData = image.ImageData,
            ObjectId = image.ObjectId,
            ObjectType = image.ObjectType
        };
    }

    private Image MapImageFromImageDto(ImageDto imageDto)
    {
        return new Image
        {
            ImageData = imageDto.ImageData,
            ObjectId = imageDto.ObjectId,
            ObjectType = imageDto.ObjectType
        };
    }

}