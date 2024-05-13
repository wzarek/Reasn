using ReasnAPI.Models.DTOs;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.Validators
{
    [TestClass]
    public class RoleValidatorTests
    {
        [TestMethod]
        public void Validate_WhenRoleNameIsEmpty_ReturnsValidationResult()
        {
            var role = new RoleDto
            {
                Name = ""
            };

            var result = RoleValidator.Validate(role);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Role name is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Role name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenRoleNameIsInvalid_ReturnsValidationResult()
        {
            var role = new RoleDto
            {
                Name = "Invalid Role"
            };

            var result = RoleValidator.Validate(role);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Role name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenRoleNameIsOrganizer_ReturnsNoValidationResult()
        {
            var role = new RoleDto
            {
                Name = "Organizer"
            };

            var result = RoleValidator.Validate(role);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Validate_WhenRoleNameIsAdmin_ReturnsNoValidationResult()
        {
            var role = new RoleDto
            {
                Name = "Admin"
            };

            var result = RoleValidator.Validate(role);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Validate_WhenRoleNameIsUser_ReturnsNoValidationResult()
        {
            var role = new RoleDto
            {
                Name = "User"
            };

            var result = RoleValidator.Validate(role);

            Assert.IsFalse(result.Any());
        }
    }
}
