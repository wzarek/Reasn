using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using ReasnAPI.Models.Enums;
using ReasnAPI.Models.Mappers;
using static System.Net.Mime.MediaTypeNames;
using Image = ReasnAPI.Models.Database.Image;
using ReasnAPI.Exceptions;

namespace ReasnAPI.Services;
public class ImageService(ReasnContext context)
{
    public List<ImageDto> CreateImages(List<ImageDto> imageDtos)
    {
        var newImages = new List<Image>();

        foreach (var imageDto in imageDtos)
        {
            var image = context.Images.FirstOrDefault(r => r.ObjectId == imageDto.ObjectId && r.ObjectType == imageDto.ObjectType && r.ImageData == imageDto.ImageData);
            if (image is not null)
            {
                continue;
            }

            var newImage = imageDto.ToEntity();
            
            newImages.Add(newImage);
        }

        if (newImages.Any())
        {
            var objectType = newImages[0].ObjectType;
          
        
            if (objectType == ObjectType.User && newImages.Count == 1)
            {
                context.Images.Add(newImages[0]);
            }
            else if (objectType == ObjectType.Event)
            {
                context.Images.AddRange(newImages);
            }
        

            context.SaveChanges();
        }

        return imageDtos;
    }

    public ImageDto UpdateImage(int imageId, ImageDto imageDto)
    {
        var image = context.Images.FirstOrDefault(r => r.Id == imageId);
        if (image is null)
        {
            throw new NotFoundException("Image not found");
        }

        image.ObjectId = imageDto.ObjectId;
        image.ImageData = imageDto.ImageData;
        image.ObjectType = imageDto.ObjectType;

        context.Images.Update(image);
        context.SaveChanges();
        return imageDto;
    }

    public void DeleteImage(int id)
    {
        var image = context.Images.FirstOrDefault(r => r.Id == id);
        if (image is null)
        {
            throw new NotFoundException("Image not found");
        }

        context.Images.Remove(image);
        context.SaveChanges();
    }

    public ImageDto GetImageById(int id)
    {
        var image = context.Images.Find(id);
        if (image is null)
        {
            throw new NotFoundException("Image not found");
        }

        var imageDto = image.ToDto();
        
        return imageDto;
    }

    public IEnumerable<ImageDto> GetAllImages()
    {
        return context.Images
            .ToDtoList()
            .AsEnumerable();
    }

    public IEnumerable<ImageDto> GetImagesByFilter(Expression<Func<Image, bool>> filter)
    {
        return context.Images
            .Where(filter)
            .ToDtoList()
            .AsEnumerable();
    }

    public IEnumerable<ImageDto> GetImagesByEventId(int eventId)
    {
        var images = context.Images
            .Where(image => image.ObjectType == ObjectType.Event && image.ObjectId == eventId)
            .ToList();

        var imageDtos = images.Select(image => image.ToDto()).AsEnumerable();

        return imageDtos;
    }

}