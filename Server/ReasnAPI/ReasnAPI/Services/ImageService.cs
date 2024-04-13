using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class ImageService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        public ImageDto CreateImage(ImageDto imageDto)
        {
            var newImage = new Image
            {
                ImageData = imageDto.ImageData,
                ObjectId = imageDto.ObjectId,
                ObjectTypeId = imageDto.ObjectTypeId
            };

            _context.Images.Add(newImage);
            _context.SaveChanges();
            return imageDto;
        }

        public ImageDto UpdateImage(int imageId,ImageDto imageDto)
        {
            var image = _context.Images.FirstOrDefault(r => r.Id == imageId);
            if (image == null)
            {
                return null;
            }

            image.ObjectId = imageDto.ObjectId;
            image.ImageData = imageDto.ImageData;
            image.ObjectTypeId = imageDto.ObjectTypeId;

            _context.Images.Update(image);
            _context.SaveChanges();
            return imageDto;
        }

        public void DeleteImage(int id)
        {
            var image = _context.Images.Find(id);
            if (image == null) { return;}
            _context.Images.Remove(image);
            _context.SaveChanges();
         
        }

        public ImageDto GetImageById(int id)
        {
            var image = _context.Images.Find(id);
            if (image == null)
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
            var images = _context.Images.ToList();
            
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
            var images = _context.Images.Where(filter).ToList();

            var imageDtos = images.Select(image => new ImageDto
            {
                ImageData = image.ImageData,
                ObjectId = image.ObjectId,
                ObjectTypeId = image.ObjectTypeId
            }).ToList();

            return imageDtos;
        }
    }
}
