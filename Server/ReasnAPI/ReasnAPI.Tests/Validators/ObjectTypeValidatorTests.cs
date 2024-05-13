using ReasnAPI.Models.Database;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.Validators
{
    [TestClass]
    public class ObjectTypeValidatorTests
    {
        [TestMethod]
        public void Validate_WhenNameIsEmpty_ReturnsValidationResult()
        {
            var objectType = new ObjectType
            {
                Name = ""
            };

            var result = ObjectTypeValidator.Validate(objectType);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenNameIsInvalid_ReturnsValidationResult()
        {
            var objectType = new ObjectType
            {
                Name = "Invalid  Name"
            };

            var result = ObjectTypeValidator.Validate(objectType);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenNameIsEvent_ReturnsNoValidationResult()
        {
            var objectType = new ObjectType
            {
                Name = "Event"
            };

            var result = ObjectTypeValidator.Validate(objectType);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Validate_WhenNameIsUser_ReturnsNoValidationResult()
        {
            var objectType = new ObjectType
            {
                Name = "User"
            };

            var result = ObjectTypeValidator.Validate(objectType);

            Assert.IsFalse(result.Any());
        }
    }
}