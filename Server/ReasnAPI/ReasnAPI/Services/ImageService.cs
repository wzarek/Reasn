using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using ReasnAPI.Exceptions;
using ReasnAPI.Models.Enums;
using ReasnAPI.Mappers;


namespace ReasnAPI.Services;
public class ImageService(ReasnContext context)
{
    public IEnumerable<ImageDto> CreateImages(List<ImageDto> imageDtos)
    {
        if (!imageDtos.Any())
        {
            throw new ArgumentException("No images provided");
        }

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

        return imageDtos.AsEnumerable();
    }

    public void UpdateImagesForEvent(int objectId, List<ImageDto> imageDtos)
    {
        if (!imageDtos.Any())
        {
            throw new ArgumentException("No images provided");
        }

        var objectType = imageDtos[0].ObjectType;

        if (objectType == ObjectType.User)
        {
            throw new ArgumentException("For User type, use UpdateImage");
        }
        else if (objectType == ObjectType.Event)
        {
            var images = context.Images.Where(r => r.ObjectId == objectId && r.ObjectType == ObjectType.Event).ToList();

            foreach (var image in images)
            {
                if (!imageDtos.Any(dto => dto.ImageData == image.ImageData))
                {
                    context.Images.Remove(image);
                }
            }

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

    public void UpdateImageForUser(int userId, ImageDto imageDto)
    {
        if (imageDto is null)
        {
            throw new ArgumentException("No image provided");
        }

        var image = context.Images.FirstOrDefault(i => i.ObjectType == ObjectType.User && i.ObjectId == userId);
        if (image is null)
        {
            throw new NotFoundException("Image not found");
        }

        image.ImageData = imageDto.ImageData;
        context.Images.Update(image);
        context.SaveChanges();

    }

    public void DeleteImageById(int id)
    {
        var image = context.Images.FirstOrDefault(r => r.Id == id);
        if (image is null)
        {
            throw new NotFoundException("Image not found");
        }

        context.Images.Remove(image);
        context.SaveChanges();
    }

    public void DeleteImageRelatedToEvent(int id, string slug)
    {
        var relatedEvent = context.Events.FirstOrDefault(r => r.Slug == slug);
        if (relatedEvent is null)
        {
            throw new NotFoundException("Event not found");
        }

        var image = context.Images.FirstOrDefault(r => r.Id == id);
        if (image is null)
        {
            throw new NotFoundException("Image not found");
        }

        if (image.ObjectId != relatedEvent.Id || image.ObjectType != ObjectType.Event)
        {
            throw new NotFoundException("This image is not related with this event");
        }

        context.Images.Remove(image);
        context.SaveChanges();
    }

    public void DeleteImageByObjectIdAndType(int objectId, ObjectType objectType)
    {
        var images = context.Images.Where(r => r.ObjectId == objectId && r.ObjectType == objectType).ToList();
        if (!images.Any())
        {
            throw new NotFoundException("Images not found");
        }

        context.Images.RemoveRange(images);
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

    public ImageDto GetImagesByUserId(int userId)
    {
        var images = context.Images
            .Where(image => image.ObjectId == userId && image.ObjectType == ObjectType.User)
            .ToList();

        if (!images.Any())
        {
            throw new NotFoundException("Images not found");
        }

        var imageDtos = images.ToDtoList();

        return imageDtos[0];
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

        if (!images.Any())
        {
            return Enumerable.Empty<ImageDto>();
        }

        var imageDtos = images.ToDtoList().AsEnumerable();

        return imageDtos;
    }

}