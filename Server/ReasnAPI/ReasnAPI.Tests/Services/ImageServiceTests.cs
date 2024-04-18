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

            var result = imageService.CreateImage(imageDto);
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

            var result = imageService.CreateImage(imageDto);
            Assert.IsNull(result);
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
            
            Assert.AreEqual(0,result.Count);
        }

        [TestMethod]
        public void GetImageById_ImageExists_ImageReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>
                { new Image { Id = 1, ImageData = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 } } });

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

            var result = imageService.GetImageById(1);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateImage_ImageExists_ImageUpdated()
        {
            var imageDto = new ImageDto
            {
                ImageData = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>
                { new Image { Id = 1, ImageData = new byte[] { } } });

            var imageService = new ImageService(mockContext.Object);

            var result = imageService.UpdateImage(1, imageDto);

            CollectionAssert.AreEqual(new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, result.ImageData);
        }

        [TestMethod]

        public void UpdateImage_ImageDoesNotExist_NullReturned()
        {
            var imageDto = new ImageDto
            {
                ImageData = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            var mockContext = new Mock<ReasnContext>();

            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>());

            var imageService = new ImageService(mockContext.Object);

            var result = imageService.UpdateImage(1, imageDto);

            Assert.IsNull(result);
        }

        [TestMethod]

        public void DeleteImage_ImageExists_ImageDeleted()
        {
            var mockContext = new Mock<ReasnContext>();

            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>
            { new Image { Id = 1, ImageData = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 } } });

            var imageService = new ImageService(mockContext.Object);

            imageService.DeleteImage(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]

        public void DeleteImage_ImageDoesNotExist_NothingDeleted()
        {
            var mockContext = new Mock<ReasnContext>();

            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>());

            var imageService = new ImageService(mockContext.Object);

            imageService.DeleteImage(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Never);
        }

        [TestMethod]

        public void GetImageByFilter_ImageExists_ImageReturned()
        {
            var mockContext = new Mock<ReasnContext>();

            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>
                { new Image { Id = 1, ImageData = new byte[] { } } });

            var imageService = new ImageService(mockContext.Object);

            var result = imageService.GetImagesByFilter(i => i.Id == 1);

            Assert.IsNotNull(result);
        }

        [TestMethod]

        public void GetImageByFilter_ImageDoesNotExist_NothingReturned()
        {
            var mockContext = new Mock<ReasnContext>();

            mockContext.Setup(c => c.Images).ReturnsDbSet(new List<Image>());

            var imageService = new ImageService(mockContext.Object);

            var result = imageService.GetImagesByFilter(i => i.Id == 1);

            Assert.AreEqual(0, result.Count());
        }


    }
}
