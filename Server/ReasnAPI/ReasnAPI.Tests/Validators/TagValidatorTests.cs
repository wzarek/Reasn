using ReasnAPI.Models.DTOs;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.Validators
{
    [TestClass]
    public class TagValidatorTests
    {
        [TestMethod]
        public void Validate_WhenNameIsEmpty_ReturnsValidationResult()
        {
            var tag = new TagDto
            {
                Name = ""
            };

            var result = TagValidator.Validate(tag);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenNameIsTooLong_ReturnsValidationResult()
        {
            var tag = new TagDto
            {
                Name = new string('a', 65)
            };

            var result = TagValidator.Validate(tag);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is too long"));
        }

        [TestMethod]
        public void Validate_WhenNameIsInvalid_ReturnsValidationResult()
        {
            var tag = new TagDto
            {
                Name = "Invalid  Name"
            };

            var result = TagValidator.Validate(tag);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenNameIsValid_ReturnsValidationResult()
        {
            var tag = new TagDto
            {
                Name = "Valid Name"
            };

            var result = TagValidator.Validate(tag);

            Assert.IsFalse(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }
    }
}