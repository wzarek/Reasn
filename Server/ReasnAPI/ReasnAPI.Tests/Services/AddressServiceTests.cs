using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services;
using Moq;
using Moq.EntityFrameworkCore;

namespace ReasnAPI.Tests.Services {
    [TestClass]
    public class AddressServiceTests {
        [TestMethod]
        public void GetAddressById_AddressExists_AddressReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address> {
                new() { Id = 1, City = "City", Country = "Country", State = "State", Street = "Street", ZipCode = "ZipCode" }
            });

            var addressService = new AddressService(mockContext.Object);

            var result = addressService.GetAddressById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("City", result.City);
            Assert.AreEqual("Country", result.Country);
            Assert.AreEqual("State", result.State);
            Assert.AreEqual("Street", result.Street);
            Assert.AreEqual("ZipCode", result.ZipCode);
        }

        [TestMethod]
        public void GetAddressById_AddressDoesNotExist_NullReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address>());

            var addressService = new AddressService(mockContext.Object);

            var result = addressService.GetAddressById(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllAddresses_AddressesExist_AddressesReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address> {
                new() { Id = 1, City = "City", Country = "Country", State = "State", Street = "Street", ZipCode = "ZipCode" },
                new() { Id = 2, City = "City", Country = "Country", State = "State", Street = "Street", ZipCode = "ZipCode" }
            });

            var addressService = new AddressService(mockContext.Object);

            var result = addressService.GetAllAddresses();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetAllAddresses_NoAddresses_EmptyListReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address>());

            var addressService = new AddressService(mockContext.Object);

            var result = addressService.GetAllAddresses();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetAddressesByFilter_AddressesExist_AddressesReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address> {
                new() { Id = 1, City = "City", Country = "Country", State = "State", Street = "Street", ZipCode = "ZipCode" },
                new() { Id = 2, City = "City", Country = "Country", State = "State", Street = "Street", ZipCode = "ZipCode" }
            });

            var addressService = new AddressService(mockContext.Object);

            var result = addressService.GetAddressesByFilter(r => r.Id == 1).ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetAddressesByFilter_NoAddresses_EmptyListReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address>());

            var addressService = new AddressService(mockContext.Object);

            var result = addressService.GetAddressesByFilter(r => r.Id == 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void CreateAddress_AddressCreated_AddressReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address>());

            var addressService = new AddressService(mockContext.Object);

            var result = addressService.CreateAddress(new AddressDto {
                City = "City",
                Country = "Country",
                State = "State",
                Street = "Street",
                ZipCode = "ZipCode"
            });

            Assert.IsNotNull(result);
            Assert.AreEqual("City", result.City);
            Assert.AreEqual("Country", result.Country);
            Assert.AreEqual("State", result.State);
            Assert.AreEqual("Street", result.Street);
            Assert.AreEqual("ZipCode", result.ZipCode);
        }

        [TestMethod]
        public void CreateAddress_AddressDtoIsNull_NullReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address>());

            var addressService = new AddressService(mockContext.Object);

            var result = addressService.CreateAddress(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateAddress_AddressUpdated_AddressReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address> {
                new() { Id = 1, City = "City", Country = "Country", State = "State", Street = "Street", ZipCode = "ZipCode" }
            });

            var addressService = new AddressService(mockContext.Object);

            var result = addressService.UpdateAddress(1, new AddressDto {
                City = "City2",
                Country = "Country2",
                State = "State2",
                Street = "Street2",
                ZipCode = "ZipCode2"
            });

            Assert.IsNotNull(result);
            Assert.AreEqual("City2", result.City);
            Assert.AreEqual("Country2", result.Country);
            Assert.AreEqual("State2", result.State);
            Assert.AreEqual("Street2", result.Street);
            Assert.AreEqual("ZipCode2", result.ZipCode);
        }

        [TestMethod]
        public void UpdateAddress_AddressDoesNotExist_NullReturned() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address>());

            var addressService = new AddressService(mockContext.Object);

            var result = addressService.UpdateAddress(1, new AddressDto {
                City = "City2",
                Country = "Country2",
                State = "State2",
                Street = "Street2",
                ZipCode = "ZipCode2"
            });

            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateAddress_AddressDtoIsNull_NullReturned() {
            var mockContext = new Mock<ReasnContext>();

            var addressService = new AddressService(mockContext.Object);

            var result = addressService.UpdateAddress(1, null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteAddress_AdddressExists_AddressDeleted() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address> {
                new() { Id = 1, City = "City", Country = "Country", State = "State", Street = "Street", ZipCode = "ZipCode" }
            });

            var addressService = new AddressService(mockContext.Object);

            addressService.DeleteAddress(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteAddress_AddressDoesNotExist_NothingHappens() {
            var mockContext = new Mock<ReasnContext>();
            mockContext.Setup(c => c.Addresses).ReturnsDbSet(new List<Address>());

            var addressService = new AddressService(mockContext.Object);

            addressService.DeleteAddress(1);

            mockContext.Verify(c => c.SaveChanges(), Times.Never);
        }
    }
}