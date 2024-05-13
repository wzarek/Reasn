using ReasnAPI.Models.DTOs;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.Validators
{
    [TestClass]
    public class CommentValidatorTests
    {
        [TestMethod]
        public void Validate_WhenContentIsEmpty_ReturnsValidationResult()
        {
            var comment = new CommentDto
            {
                Content = "",
                CreatedAt = DateTime.Now
            };

            var result = CommentValidator.Validate(comment);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Content is required"));
        }

        [TestMethod]
        public void Validate_WhenContentIsTooLong_ReturnsValidationResult()
        {
            var comment = new CommentDto
            {
                Content = new string('a', 1025),
                CreatedAt = DateTime.Now
            };

            var result = CommentValidator.Validate(comment);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "Content is too long"));
        }

        [TestMethod]
        public void Validate_WhenCreatedAtIsInTheFuture_ReturnsValidationResult()
        {
            var comment = new CommentDto
            {
                Content = "Content",
                CreatedAt = DateTime.Now.AddMinutes(1)
            };

            var result = CommentValidator.Validate(comment);

            Assert.IsTrue(result.Any(r => r.ErrorMessage == "CreatedAt is in the future"));
        }

        [TestMethod]
        public void Validate_WhenCommentIsValid_ReturnsNoValidationResult()
        {
            var comment = new CommentDto
            {
                Content = "Content",
                CreatedAt = DateTime.Now
            };

            var result = CommentValidator.Validate(comment);

            Assert.IsFalse(result.Any());
        }
    }
}