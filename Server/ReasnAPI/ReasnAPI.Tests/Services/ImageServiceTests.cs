using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Moq.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Models.Enums;


namespace ReasnAPI.Tests.Services
{
    [TestClass]
    public class ImageServiceTests
    {

        [TestMethod]
        public void CreateImage_ImageDoesNotExist_ImageCreated()
        {
            var imageDto = new ImageDto
            {
                ImageData = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>());

            var imageService = new ImageService(mockContext.Object);
            List<ImageDto> imagedtoslist = new List<ImageDto>();
            imagedtoslist.Add(imageDto); // Use Add instead of Append

            var result = imageService.CreateImages(imagedtoslist);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateImage_ImageExists_ImageNotCreated()
        {
            var imageDto = new ImageDto
            {
                ImageData = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>
                { new Image { ImageData = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 } } });

            var imageService = new ImageService(mockContext.Object);
            List<ImageDto> imagedtoslist = new List<ImageDto>();
            imagedtoslist.Add(imageDto); // Use Add instead of Append

            var result = imageService.CreateImages(imagedtoslist);

            mockContext.Verify(m => m.Add(It.IsAny<Image>()), Times.Never());
            mockContext.Verify(m => m.AddRange(It.IsAny<IEnumerable<Image>>()), Times.Never());
        }

        [TestMethod]
        public void GetAllImages_ImageExists_ImagesReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>
                { new Image { ImageData = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 } } });

            var imageService = new ImageService(mockContext.Object);

            var result = imageService.GetAllImages();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllImages_ImageNotExists_EmptyListReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>());

            var imageService = new ImageService(mockContext.Object);

            var result = imageService.GetAllImages().ToList();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetImageById_ImageExists_ImageReturned()
        {

            var imaDto = new ImageDto()
            {
                ImageData = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            var fakeStatuses = new FakeDbSet<Image>();
            fakeStatuses.Add(new Image() { Id = 1, ImageData = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 } });

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Images).Returns(fakeStatuses);

            var imageService = new ImageService(mockContext.Object);

            var result = imageService.GetImageById(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetImageById_ImageDoesNotExist_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>());

            var imageService = new ImageService(mockContext.Object);

            Assert.ThrowsException<NotFoundException>(() => imageService.GetImageById(1));
        }

        [TestMethod]
        public void UpdateImages_UserType_ImageUpdated()
        {
            var imageDto = new ImageDto
            {
                ObjectId = 1,
                ObjectType = ObjectType.User,
                ImageData = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>
        { new Image { ObjectId = 1, ObjectType = ObjectType.User, ImageData = new byte[] { } } });

            var imageService = new ImageService(mockContext.Object);

            imageService.UpdateImageForUser(1, imageDto);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void UpdateImages_EventType_ImagesUpdated()
        {
            var imageDtos = new List<ImageDto>
    {
        new ImageDto
        {
            ObjectId = 1,
            ObjectType = ObjectType.Event,
            ImageData = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
        },
        new ImageDto
        {
            ObjectId = 1,
            ObjectType = ObjectType.Event,
            ImageData = new byte[] { 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30 }
        }
    };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>
        { new Image { ObjectId = 1, ObjectType = ObjectType.Event, ImageData = new byte[] { } } });

            var imageService = new ImageService(mockContext.Object);

            imageService.UpdateImagesForEvent(1, imageDtos);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void UpdateImages_ImageDoesNotExist_ExceptionThrown()
        {
            var imageDto = new ImageDto
            {
                ObjectId = 1,
                ObjectType = ObjectType.User,
                ImageData = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>());

            var imageService = new ImageService(mockContext.Object);

            Assert.ThrowsException<ArgumentException>(() => imageService.UpdateImagesForEvent(1, new List<ImageDto> { imageDto }));
        }

        [TestMethod]

        public void DeleteImage_ImageExists_ImageDeleted()
        {
            var mockContext = new Mock<ReasnContext>();

            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>
            { new Image { Id = 1, ImageData = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 } } });

            var imageService = new ImageService(mockContext.Object);

            imageService.DeleteImageById(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]

        public void DeleteImage_ImageDoesNotExist_NothingDeleted()
        {
            var mockContext = new Mock<ReasnContext>();

            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>());

            var imageService = new ImageService(mockContext.Object);

            Assert.ThrowsException<NotFoundException>(() => imageService.DeleteImageById(1));
        }

        [TestMethod]

        public void GetImageByFilter_ImageExists_ImageReturned()
        {
            var mockContext = new Mock<ReasnContext>();

            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>
                { new Image { Id = 1, ImageData = new byte[] { } } });

            var imageService = new ImageService(mockContext.Object);

            var result = imageService.GetImagesByFilter(i => i.Id == 1).ToList();

            Assert.IsNotNull(result);
        }

        [TestMethod]

        public void GetImageByFilter_ImageDoesNotExist_NothingReturned()
        {
            var mockContext = new Mock<ReasnContext>();

            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>());

            var imageService = new ImageService(mockContext.Object);

            var result = imageService.GetImagesByFilter(i => i.Id == 1).ToList();

            Assert.AreEqual(0, result.Count());
        }


    }
}
