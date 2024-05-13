using ReasnAPI.Models.DTOs;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.Validators
{
    [TestClass]
    public class AddressValidatorTests
    {
        [TestMethod]
        public void Validate_WhenCityIsEmpty_ReturnsValidationResult()
        {
            var address = new AddressDto
            {
                City = "",
                Country = "Country",
                Street = "Street",
                State = "State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "City is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "City is invalid"));
        }

        [TestMethod]
        public void Validate_WhenCityIsTooLong_ReturnsValidationResult()
        {
            var address = new AddressDto
            {
                City = new string('a', 65),
                Country = "Country",
                Street = "Street",
                State = "State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "City is too long"));
        }

        [TestMethod]
        public void Validate_WhenCityIsInvalid_ReturnsValidationResult()
        {
            var address = new AddressDto
            {
                City = "Invalid  City",
                Country = "Country",
                Street = "Street",
                State = "State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "City is invalid"));
        }

        [TestMethod]
        public void Validate_WhenCityIsValid_ReturnsNoValidationResult()
        {
            var address = new AddressDto
            {
                City = "Valid City",
                Country = "Country",
                Street = "Street",
                State = "State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Validate_WhenCountryIsEmpty_ReturnsValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = "",
                Street = "Street",
                State = "State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Country is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Country is invalid"));
        }

        [TestMethod]
        public void Validate_WhenCountryIsTooLong_ReturnsValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = new string('a', 65),
                Street = "Street",
                State = "State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Country is too long"));
        }

        [TestMethod]
        public void Validate_WhenCountryIsInvalid_ReturnsValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = "Invalid  Country",
                Street = "Street",
                State = "State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Country is invalid"));
        }

        [TestMethod]
        public void Validate_WhenCountryIsValid_ReturnsNoValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = "Valid Country",
                Street = "Street",
                State = "State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Validate_WhenStreetIsEmpty_ReturnsValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = "Country",
                Street = "",
                State = "State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Street is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Street is invalid"));
        }

        [TestMethod]
        public void Validate_WhenStreetIsTooLong_ReturnsValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = "Country",
                Street = new string('a', 65),
                State = "State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Street is too long"));
        }

        [TestMethod]
        public void Validate_WhenStreetIsInvalid_ReturnsValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = "Country",
                Street = "Invalid  Street",
                State = "State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Street is invalid"));
        }

        [TestMethod]
        public void Validate_WhenStreetIsValid_ReturnsNoValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = "Country",
                Street = "Valid Street",
                State = "State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Validate_WhenStateIsEmpty_ReturnsValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = "Country",
                Street = "Street",
                State = "",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "State is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "State is invalid"));
        }

        [TestMethod]
        public void Validate_WhenStateIsTooLong_ReturnsValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = "Country",
                Street = "Street",
                State = new string('a', 65),
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "State is too long"));
        }

        [TestMethod]
        public void Validate_WhenStateIsInvalid_ReturnsValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = "Country",
                Street = "Street",
                State = "Invalid  State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "State is invalid"));
        }

        [TestMethod]
        public void Validate_WhenStateIsValid_ReturnsNoValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = "Country",
                Street = "Street",
                State = "Valid State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Valdate_WhenZipCodeIsTooLong_ReturnsValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = "Country",
                Street = "Street",
                State = "State",
                ZipCode = new string('0', 9)
            };

            var result = AddressValidator.Validate(address);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "ZipCode is too long"));
        }

        [TestMethod]
        public void Validate_WhenZipCodeIsInvalid_ReturnsValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = "Country",
                Street = "Street",
                State = "State",
                ZipCode = "Invalid_"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "ZipCode is invalid"));
        }

        [TestMethod]
        public void Validate_WhenZipCodeIsValid_ReturnsNoValidationResult()
        {
            var address = new AddressDto
            {
                City = "City",
                Country = "Country",
                Street = "Street",
                State = "State",
                ZipCode = "00-000"
            };

            var result = AddressValidator.Validate(address);

            Assert.IsFalse(result.Any());
        }
    }
}