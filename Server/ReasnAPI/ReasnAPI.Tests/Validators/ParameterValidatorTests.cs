using ReasnAPI.Models.DTOs;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.Validators
{
    [TestClass]
    public class ParameterValidatorTests
    {
        [TestMethod]
        public void Validate_WhenKeyIsEmpty_ReturnsKeyIsRequired()
        {
            var parameter = new ParameterDto
            {
                Key = "",
                Value = "value"
            };

            var result = ParameterValidator.Validate(parameter);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Key is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Key is invalid"));
        }

        [TestMethod]
        public void Validate_WhenKeyIsTooLong_ReturnsKeyIsTooLong()
        {
            var parameter = new ParameterDto
            {
                Key = new string('a', 33),
                Value = "value"
            };

            var result = ParameterValidator.Validate(parameter);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Key is too long"));
        }

        [TestMethod]
        public void Validate_WhenKeyIsInvalid_ReturnsKeyIsInvalid()
        {
            var parameter = new ParameterDto
            {
                Key = "Invalid  Key",
                Value = "value"
            };

            var result = ParameterValidator.Validate(parameter);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Key is invalid"));
        }

        [TestMethod]
        public void Validate_WhenValueIsEmpty_ReturnsValueIsRequired()
        {
            var parameter = new ParameterDto
            {
                Key = "key",
                Value = ""
            };

            var result = ParameterValidator.Validate(parameter);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Value is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Value is invalid"));
        }

        [TestMethod]
        public void Validate_WhenValueIsTooLong_ReturnsValueIsTooLong()
        {
            var parameter = new ParameterDto
            {
                Key = "key",
                Value = new string('a', 65)
            };

            var result = ParameterValidator.Validate(parameter);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Value is too long"));
        }

        [TestMethod]
        public void Validate_WhenValueIsInvalid_ReturnsValueIsInvalid()
        {
            var parameter = new ParameterDto
            {
                Key = "key",
                Value = "Invalid  Value"
            };

            var result = ParameterValidator.Validate(parameter);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Value is invalid"));
        }

        [TestMethod]
        public void Validate_WhenValueAndKeyAreValid_ReturnsNoValidationResult()
        {
            var parameter = new ParameterDto
            {
                Key = "key",
                Value = "value"
            };

            var result = ParameterValidator.Validate(parameter);

            Assert.IsFalse(result.Any());
        }
    }
}
