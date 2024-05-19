namespace ReasnAPI.Models.DTOs
{
    public class UserInterestDto
    {
        public InterestDto Interest { get; set; } = null!;
        public int Level { get; set; }
    }
}
