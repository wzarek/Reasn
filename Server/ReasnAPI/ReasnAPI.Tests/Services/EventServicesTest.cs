using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services;
using ReasnAPI.Models.Database;
using Moq;
using Moq.EntityFrameworkCore;
using ReasnAPI.Models.Enums;


namespace ReasnAPI.Tests.Services
{
    [TestClass]
    public class EventServicesTest
    {
    //    [TestMethod]
    //    public void CreateEvent_EventDoesNotExist_EventCreated()
    //    {
    //        // Arrange
    //        var tagDto = new TagDto
    //        {
    //            Name = "testTag"
    //        };
    //        var tagList = new List<TagDto> { tagDto };

    //        var eventDto = new EventDto
    //        {
    //            Name = "name",
    //            AddressId = 1,
    //            Description = "description",
    //            OrganizerId = 1,
    //            StartAt = DateTime.Now,
    //            EndAt = DateTime.Now,
    //            CreatedAt = DateTime.Now,
    //            UpdatedAt = DateTime.Now,
    //            Status = EventStatus.Approved,
    //            Tags = tagList,
    //        };

    //        var mockContext = new Mock<ReasnContext>();
    //        var events = new List<Event>();
    //        var tags = new List<Tag>();

    //        mockContext.Setup(c => c.Parameters).ReturnsDbSet(new List<Parameter>());
    //        mockContext.Setup(c => c.Events).ReturnsDbSet(new List<Event>());
    //        mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag>());

    //        mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address>
    //{
    //    new Address
    //    {
    //        Id = 1,
    //        City = "city",
    //        Country = "country",
    //        State = "state",
    //        Street = "street",
    //        ZipCode = "test123"
    //    }
    //});

    //        mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>
    //{
    //    new User
    //    {
    //        Id = 1,
    //        Name = "test",
    //        Email = "test@wp.pl",
    //        AddressId = 1,
    //        CreatedAt = DateTime.Now,
    //        IsActive = true,
    //        Role = UserRole.Admin,
    //        Password = "test123",
    //        Phone = "123123123",
    //        Surname = "test",
    //        Username = "test",
    //        UpdatedAt = DateTime.Now
    //    }
    //});

    //        var eventService = new EventService(mockContext.Object);

    //        // Act
    //        var result = eventService.CreateEvent(eventDto);

    //        // Assert
    //        Assert.IsNotNull(result);
    //    }

        [TestMethod]
        public void UpdateEvent_EventExists_EventUpdated()
        {
            var tagDto = new TagDto() { Name = "tesTag" };
            var tagList = new List<TagDto>
            {
                tagDto
            };
            var eventDto = new EventDto()
            {
                Name = "name1",
                AddressId = 1,
                Description = "description2",
                OrganizerId = 1,
                StartAt = DateTime.Now,
                EndAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = EventStatus.Completed,
                Tags = tagList,
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Events).ReturnsDbSet(new List<Event>{ 
                new Event()
            {
                Id = 1,
                Name = "name",
                AddressId = 1,
                Description = "description",
                OrganizerId = 1,
                StartAt = DateTime.Now,
                EndAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = EventStatus.Completed,
            }});
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag>());
            
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address>{
                new Address()
                {
                    Id = 1, 
                    City = "city", 
                    Country = "country", 
                    State = "state", 
                    Street = "street", 
                    ZipCode = "test123"
                }});
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>{ 
                new User()
            {
                Id = 1, 
                Name = "test", 
                Email = "test@wp.pl", 
                AddressId  = 1, 
                CreatedAt = DateTime.Now,
                IsActive = true, Role = UserRole.Admin, 
                Password ="test123",
                Phone = "123123123", 
                Surname ="test", 
                Username ="test", 
                UpdatedAt =DateTime.Now }});
            
            var eventService = new EventService(mockContext.Object);

            var result = eventService.UpdateEvent(1,eventDto);
            Assert.AreEqual("name1",result.Name);
            Assert.AreEqual("description2",result.Description);
            Assert.AreEqual(1, result.Tags.Count);

        }

        [TestMethod]
        public void UpdateEvent_EventDoesNotExist_NullReturned()
        {
            var tagDto = new TagDto() { Name = "tesTag" };
            var tagList = new List<TagDto>
            {
                tagDto
            };
            var eventDto = new EventDto()
            {
                Name = "name1",
                AddressId = 1,
                Description = "description2",
                OrganizerId = 1,
                StartAt = DateTime.Now,
                EndAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = EventStatus.Completed,
                Tags = tagList,
            };

            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Events).ReturnsDbSet(new List<Event>());
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag>());
         
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address>{
                new Address()
            {
                Id = 1,
                City = "city",
                Country = "country", 
                State = "state", 
                Street = "street", 
                ZipCode = "test123"
            }});
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>{ 
                new User()
            {
                Id = 1, 
                Name = "test", 
                Email = "test@wp.pl", 
                AddressId  = 1, 
                CreatedAt = DateTime.Now,
                IsActive = true, 
                Role = UserRole.Admin, 
                Password ="test123",
                Phone = "123123123",
                Surname ="test",
                Username ="test", 
                UpdatedAt =DateTime.Now
            }});
            
            var eventService = new EventService(mockContext.Object);

            var result = eventService.UpdateEvent(1, eventDto);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetEventById_EventExists_EventReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Events).ReturnsDbSet(new List<Event>
            {
                new Event()
                {
                Id = 1,
                Name = "name",
                Slug = "name",
                AddressId = 1,
                Description = "description",
                OrganizerId = 1,
                StartAt = DateTime.Now,
                EndAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = EventStatus.Completed,
            }});
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag>
            {
                new Tag()
                {
                    Id = 1, 
                    Name = "name"
                }
            });
  
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address>{
                new Address()
            {
                Id = 1, 
                City = "city", 
                Country = "country", 
                State = "state",
                Street = "street", 
                ZipCode = "test123"
            }});
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>{ 
                new User()
            {
                Id = 1, 
                Name = "test",
                Email = "test@wp.pl",
                AddressId  = 1, 
                CreatedAt = DateTime.Now,
                IsActive = true, 
                Role = UserRole.User,
                Password ="test123",
                Phone = "123123123",
                Surname ="test",
                Username ="test",
                UpdatedAt =DateTime.Now }});


            var eventService = new EventService(mockContext.Object);

            var result = eventService.GetEventById(1);
            Assert.IsNotNull(result);
           
        }

        [TestMethod]

        public void GetEventById_EventDoesNotExist_NullReturned()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Events).ReturnsDbSet(new List<Event>());
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag>());

            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address>
            {
                new Address()
                {
                Id = 1, 
                City = "city", 
                Country = "country", 
                State = "state", 
                Street = "street", 
                ZipCode = "test123"
            }});
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>
            {
                new User()
                {
                    Id = 1,
                    Name = "test",
                    Email = "test@wp.pl",
                    AddressId  = 1,
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                    Role = UserRole.Admin,
                    Password ="test123",
                    Phone = "123123123",
                    Surname ="test",
                    Username ="test",
                    UpdatedAt =DateTime.Now
                }});


            var eventService = new EventService(mockContext.Object);

            var result = eventService.GetEventById(1);

            Assert.IsNull(result);
        }

        [TestMethod]

        public void DeleteEvent_EventExists_EventDeleted()
        {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Events).ReturnsDbSet(new List<Event>
            {
                new Event()
                {
                    Id = 1,
                    Name = "name",
                    Slug = "name",
                    AddressId = 1,
                    Description = "description",
                    OrganizerId = 1,
                    StartAt = DateTime.Now,
                    EndAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = EventStatus.Completed,
                }
            });
            mockContext.Setup(c => c.Tags).ReturnsDbSet(new List<Tag>
            {
                new Tag()
                {
                    Id = 1,
                    Name = "name"
                }
            });
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address>
            {
                new Address()
                {
                    Id = 1,
                    City = "city",
                    Country = "country",
                    State = "state",
                    Street = "street",
                    ZipCode = "test123"
                }
            });
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>{
                new User()
                {
                    Id = 1,
                    Name = "test",
                    Email = "test@wp.pl",
                    AddressId  = 1,
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                    Role = UserRole.User,
                    Password ="test123",
                    Phone = "123123123",
                    Surname ="test",
                    Username ="test",
                    UpdatedAt =DateTime.Now }});
            
        

            var eventService = new EventService(mockContext.Object);

            eventService.DeleteEvent(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

    }
}
