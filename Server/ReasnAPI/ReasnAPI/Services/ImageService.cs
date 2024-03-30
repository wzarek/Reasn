using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Services {
    public class ImageService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        /* TODO: Create following functions for this class
         * create
         * update
         * delete
         * get by ID
         * get list by filter
         * get all
         */

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

        public bool DeleteImage(int id)
        {
            var image = _context.Images.Find(id);
            if (image == null)
            {
                return false;
            }

            _context.Images.Remove(image);
            _context.SaveChanges();
            return true;
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

        public List<ImageDto> GetImagesByFilter(int objectTypeId, int objectId)
        {
            var images = _context.Images.Where(r => r.ObjectTypeId == objectTypeId && r.ObjectId == objectId).ToList();
            if (images == null)
            {
                return null;
            }

            var imageDtos = images.Select(image => new ImageDto
            {
                ImageData = image.ImageData,
                ObjectId = image.ObjectId,
                ObjectTypeId = image.ObjectTypeId
            }).ToList();

            return imageDtos;
        }

        public List<ImageDto> GetAllImages()
        {
            var images = _context.Images.ToList();
            if (images == null)
            {
                return null;
            }

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
