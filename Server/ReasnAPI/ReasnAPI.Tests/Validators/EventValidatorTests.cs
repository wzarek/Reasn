using ReasnAPI.Models.DTOs;
using ReasnAPI.Validators;
using System;

namespace ReasnAPI.Tests.Validators
{
    [TestClass]
    public class EventValidatorTests
    {
        [TestMethod]
        public void Validate_WhenNameIsEmpty_ReturnsValidationResult()
        {
            var event1 = new EventDto
            {
                Name = "",
                Description = "Description",
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddMinutes(1),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Slug = "slug"
            };

            var result = EventValidator.Validate(event1);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is required"));
        }

        [TestMethod]
        public void Validate_WhenNameIsTooLong_ReturnsValidationResult()
        {
            var event1 = new EventDto
            {
                Name = new string('a', 65),
                Description = "Description",
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddMinutes(1),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Slug = "slug"
            };

            var result = EventValidator.Validate(event1);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Name is too long"));
        }

        [TestMethod]
        public void Validate_WhenNameIsValid_ReturnsNoValidationResult()
        {
            var event1 = new EventDto
            {
                Name = "ValidName",
                Description = "Description",
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddMinutes(1),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Slug = "slug"
            };

            var result = EventValidator.Validate(event1);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Validate_WhenDescriptionIsEmpty_ReturnsValidationResult()
        {
            var event1 = new EventDto
            {
                Name = "Name",
                Description = "",
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddMinutes(1),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Slug = "slug"
            };

            var result = EventValidator.Validate(event1);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Description is required"));
        }

        [TestMethod]
        public void Validate_WhenDescriptionIsTooLong_ReturnsValidationResult()
        {
            var event1 = new EventDto
            {
                Name = "Name",
                Description = new string('a', 4049),
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddMinutes(1),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Slug = "slug"
            };

            var result = EventValidator.Validate(event1);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Description is too long"));
        }

        [TestMethod]
        public void Validate_WhenDescriptionIsValid_ReturnsNoValidationResult()
        {
            var event1 = new EventDto
            {
                Name = "Name",
                Description = "Valid Description",
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddMinutes(1),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Slug = "slug"
            };

            var result = EventValidator.Validate(event1);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Validate_WhenStartAtIsAfterEndAt_ReturnsValidationResult()
        {
            var event1 = new EventDto
            {
                Name = "Name",
                Description = "Description",
                StartAt = DateTime.Now.AddMinutes(1),
                EndAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Slug = "slug"
            };

            var result = EventValidator.Validate(event1);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "StartTime is after EndTime"));
        }

        [TestMethod]
        public void Validate_WhenCreatedAtIsInTheFuture_ReturnsValidationResult()
        {
            var event1 = new EventDto
            {
                Name = "Name",
                Description = "Description",
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddMinutes(1),
                CreatedAt = DateTime.Now.AddMinutes(1),
                UpdatedAt = DateTime.Now,
                Slug = "slug"
            };

            var result = EventValidator.Validate(event1);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "CreatedAt is in the future"));
        }

        [TestMethod]
        public void Validate_WhenUpdatedAtIsInTheFuture_ReturnsValidationResult()
        {
            var event1 = new EventDto
            {
                Name = "Name",
                Description = "Description",
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddMinutes(1),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now.AddMinutes(1),
                Slug = "slug"
            };

            var result = EventValidator.Validate(event1);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "UpdatedAt is in the future"));
        }

        [TestMethod]
        public void Validate_WhenSlugIsEmpty_ReturnsValidationResult()
        {
            var event1 = new EventDto
            {
                Name = "Name",
                Description = "Description",
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddMinutes(1),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Slug = ""
            };

            var result = EventValidator.Validate(event1);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Slug is required"));
            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Slug is invalid"));
        }

        [TestMethod]
        public void Validate_WhenSlugIsTooLong_ReturnsValidationResult()
        {
            var event1 = new EventDto
            {
                Name = "Name",
                Description = "Description",
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddMinutes(1),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Slug = new string('a', 129)
            };

            var result = EventValidator.Validate(event1);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Slug is too long"));
        }

        [TestMethod]
        public void Validate_WhenSlugIsIsInvalid_ReturnsValidationResult()
        {
            var event1 = new EventDto
            {
                Name = "Name",
                Description = "Description",
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddMinutes(1),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Slug = "invalid slug"
            };

            var result = EventValidator.Validate(event1);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Slug is invalid"));
        }

        [TestMethod]
        public void Validate_WhenSlugIsValid_ReturnsNoValidationResult()
        {
            var event1 = new EventDto
            {
                Name = "Name",
                Description = "Description",
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddMinutes(1),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Slug = "valid-slug"
            };

            var result = EventValidator.Validate(event1);

            Assert.IsFalse(result.Any());
        }
    }
}