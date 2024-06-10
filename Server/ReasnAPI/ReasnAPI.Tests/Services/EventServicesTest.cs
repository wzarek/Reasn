using ReasnAPI.Models.DTOs;
using ReasnAPI.Services;
using ReasnAPI.Models.Database;
using Moq;
using Moq.EntityFrameworkCore;
using ReasnAPI.Models.Enums;
using ReasnAPI.Exceptions;


namespace ReasnAPI.Tests.Services
{
    [TestClass]
    public class EventServicesTest
    {

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

            Assert.ThrowsException<NotFoundException>(() => eventService.UpdateEvent(1, eventDto));
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

            Assert.ThrowsException<NotFoundException>(() => eventService.GetEventById(1));
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
            Tags = new List<Tag> { new Tag { Id = 1, Name = "name" } },
            Parameters = new List<Parameter> { new Parameter { Key = "key", Value = "value" } },
            Comments = new List<Comment> { new Comment { Id = 1, Content = "content" } },
            Participants = new List<Participant> { new Participant { Id = 1, UserId = 1 } }
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
            mockContext.Setup(c => c.Parameters).ReturnsDbSet(new List<Parameter>());
            mockContext.Setup(c => c.Comments).ReturnsDbSet(new List<Comment>());
            mockContext.Setup(c => c.Participants).ReturnsDbSet(new List<Participant>());

            var eventService = new EventService(mockContext.Object);

            eventService.DeleteEvent(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

    }
}
