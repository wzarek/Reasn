namespace ReasnAPI.Models.API
{
    public class Organizer(string username, string photo)
    {
        public string Username { get; set; } = username;
        public string Photo { get; set; } = photo;
    }
}
