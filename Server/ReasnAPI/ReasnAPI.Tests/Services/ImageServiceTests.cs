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

    }
}
