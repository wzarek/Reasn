using ReasnAPI.Models.DTOs;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.Validators
{
    [TestClass]
    public class StatusValidatorTests
    {
        [TestMethod]
        public void Validate_WhenNameIsEmpty_ReturnsValidationResult()
        {
            var status = new StatusDto
            {
                Name = ""
            };

            var result = StatusValidator.Validate(status);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenNameIsInvalid_ReturnsValidationResult()
        {
            var status = new StatusDto
            {
                Name = "Invalid Name"
            };

            var result = StatusValidator.Validate(status);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenNameIsInterested_ReturnsValidationResult()
        {
            var status = new StatusDto
            {
                Name = "Interested"
            };

            var result = StatusValidator.Validate(status);

            Assert.IsFalse(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenNameIsParticipating_ReturnsValidationResult()
        {
            var status = new StatusDto
            {
                Name = "Participating"
            };

            var result = StatusValidator.Validate(status);

            Assert.IsFalse(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenNameIsCompleted_ReturnsValidationResult()
        {
            var status = new StatusDto
            {
                Name = "Completed"
            };

            var result = StatusValidator.Validate(status);

            Assert.IsFalse(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenNameIsInProgress_ReturnsValidationResult()
        {
            var status = new StatusDto
            {
                Name = "In progress"
            };

            var result = StatusValidator.Validate(status);

            Assert.IsFalse(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }

        [TestMethod]
        public void Validate_WhenNameIsWaitingForApproval_ReturnsValidationResult()
        {
            var status = new StatusDto
            {
                Name = "Waiting for approval"
            };

            var result = StatusValidator.Validate(status);

            Assert.IsFalse(result.Any(r => r.ErrorMessage == "Name is invalid"));
        }
    }
}