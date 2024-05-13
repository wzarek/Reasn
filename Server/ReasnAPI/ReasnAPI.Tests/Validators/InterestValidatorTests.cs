using ReasnAPI.Models.DTOs;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.Validators
{
    [TestClass]
    public class InterestValidatorTests
    {
        [TestMethod]
        public void Validate_WhenNameIsEmpty_ReturnsValidationResult()
        {
            var interest = new IntrestDto
            {
                Name = ""
            };

            var result = InterestValidator.Validate(interest);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenNameIsTooLong_ReturnsValidationResult()
        {
            var interest = new IntrestDto
            {
                Name = new string('a', 33)
            };

            var result = InterestValidator.Validate(interest);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is too long"));
        }

        [TestMethod]
        public void Validate_WhenNameIsInvalid_ReturnsValidationResult()
        {
            var interest = new IntrestDto
            {
                Name = "Invalid  Name"
            };

            var result = InterestValidator.Validate(interest);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenNameIsValid_ReturnsNoValidationResult()
        {
            var interest = new IntrestDto
            {
                Name = "Valid Name"
            };

            var result = InterestValidator.Validate(interest);

            Assert.IsFalse(result.Any());
        }
    }
}
