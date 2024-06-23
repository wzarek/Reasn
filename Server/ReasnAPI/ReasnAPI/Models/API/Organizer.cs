namespace ReasnAPI.Models.API
{
    public class Organizer(string username, string image)
    {
        public string Username { get; set; } = username;
        public string Image { get; set; } = image;
    }
}
