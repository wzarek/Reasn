using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using ReasnAPI.Models.Enums;
using ReasnAPI.Models.Mappers;
using ReasnAPI.Exceptions;

namespace ReasnAPI.Services;
public class ImageService(ReasnContext context)
{
    public List<ImageDto> CreateImages(List<ImageDto> imageDtos)
    {
        var newImages = new List<Image>();

        if (imageDtos[0].ObjectType == ObjectType.User && imageDtos.Count > 1)
        {
            throw new ArgumentException("For User type, only one image can be created");
        }

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

    public void UpdateImages(int objectId, List<ImageDto> imageDtos)
    {
        if (!imageDtos.Any())
        {
            throw new ArgumentException("No images provided");
        }

        var objectType = imageDtos[0].ObjectType;

        if (objectType == ObjectType.User)
        {
            if (imageDtos.Count != 1)
            {
                throw new ArgumentException("For User type, only one image can be updated");
            }

            var image = context.Images.FirstOrDefault(r => r.ObjectId == objectId && r.ObjectType == ObjectType.User);
            if (image is null)
            {
                throw new NotFoundException("Image not found");
            }

            image.ImageData = imageDtos[0].ImageData;

            context.Images.Update(image);
        }
        else if (objectType == ObjectType.Event)
        {
            var images = context.Images.Where(r => r.ObjectId == objectId && r.ObjectType == ObjectType.Event).ToList();

            // Remove images that are not in the new list
            foreach (var image in images)
            {
                if (!imageDtos.Any(dto => dto.ImageData == image.ImageData))
                {
                    context.Images.Remove(image);
                }
            }

            // Add new images that are not in the database
            var newImages = new List<Image>();
            foreach (var imageDto in imageDtos)
            {
                if (!images.Any(img => img.ImageData == imageDto.ImageData))
                {
                    var newImage = imageDto.ToEntity();
                    newImages.Add(newImage);
                }
            }
            context.Images.AddRange(newImages);
        }

        context.SaveChanges();
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

    public List<ImageDto> GetImagesByObjectId(int objectId, ObjectType objectType)
    {
        var images = context.Images
            .Where(image => image.ObjectId == objectId && image.ObjectType == objectType)
            .ToList();

        if (!images.Any())
        {
            throw new NotFoundException("Images not found");
        }

        var imageDtos = images.Select(image => image.ToDto()).ToList();

        return imageDtos;
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