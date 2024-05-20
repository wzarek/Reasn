using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Enums;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.Validators
{
    [TestClass]
    public class UserValidatorTests
    {
        [TestMethod]
        public void Validate_WhenNameIsEmpty_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = "",
                Surname = "Surname",
                Username = "Username",
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenNameIsTooLong_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = new string('a', 65),
                Surname = "Surname",
                Username = "Username",
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is too long"));
        }

        [TestMethod]
        public void Validate_WhenNameIsInvalid_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = "Invalid  Name",
                Surname = "Surname",
                Username = "Username",
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenNameIsValid_ReturnsNoValidationResult()
        {
            var user = new UserDto
            {
                Name = "Validname",
                Surname = "Surname",
                Username = "Username",
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Validate_WhenSurnameIsEmpty_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = "",
                Username = "Username",
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Surname is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Surname is invalid"));
        }

        [TestMethod]
        public void Validate_WhenSurnameIsTooLong_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = new string('a', 65),
                Username = "Username",
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Surname is too long"));
        }

        [TestMethod]
        public void Validate_WhenSurnameIsInvalid_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = "Invalid  Surname",
                Username = "Username",
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Surname is invalid"));
        }

        [TestMethod]
        public void Validate_WhenSurnameIsValid_ReturnsNoValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = "Validsurname",
                Username = "Username",
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Validate_WhenUsernameIsEmpty_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = "Surname",
                Username = "",
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Username is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Username is invalid"));
        }

        [TestMethod]
        public void Validate_WhenUsernameIsTooLong_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = "Surname",
                Username = new string('a', 65),
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Username is too long"));
        }

        [TestMethod]
        public void Validate_WhenUsernameIsInvalid_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = "Surname",
                Username = "Invalid  Username",
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Username is invalid"));
        }

        [TestMethod]
        public void Validate_WhenUsernameIsValid_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = "Surname",
                Username = "ValidUsername",
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Validate_WhenEmailIsEmpty_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = "Surname",
                Username = "Username",
                Role = UserRole.User,
                Email = "",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Email is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Email is invalid"));
        }

        [TestMethod]
        public void Validate_WhenEmailIsTooLong_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = "Surname",
                Username = "Username",
                Role = UserRole.User,
                Email = new string('a', 256),
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Email is too long"));
        }

        [TestMethod]
        public void Validate_WhenEmailIsInvalid_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = "Surname",
                Username = "Username",
                Role = UserRole.User,
                Email = "Invalid Email",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Email is invalid"));
        }

        [TestMethod]
        public void Validate_WhenEmailIsValid_ReturnsNoValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = "Surname",
                Username = "Username",
                Role = UserRole.User,
                Email = "valid@email.com",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Validate_WhenPhoneIsTooLong_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = "Surname",
                Username = "Username",
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = new string('a', 20)
            };

            var result = UserValidator.Validate(user);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Phone is too long"));
        }

        [TestMethod]
        public void Validate_WhenPhoneIsInvalid_ReturnsValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = "Surname",
                Username = "Username",
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = "Invalid Phone"
            };

            var result = UserValidator.Validate(user);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Phone is invalid"));
        }

        [TestMethod]
        public void Validate_WhenPhoneIsValid_ReturnsNoValidationResult()
        {
            var user = new UserDto
            {
                Name = "Name",
                Surname = "Surname",
                Username = "Username",
                Role = UserRole.User,
                Email = "email@email.com",
                AddressId = 1,
                Phone = "+48 123456789"
            };

            var result = UserValidator.Validate(user);

            Assert.IsFalse(result.Any());
        }
    }
}