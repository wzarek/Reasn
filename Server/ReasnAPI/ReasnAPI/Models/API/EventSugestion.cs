namespace ReasnAPI.Models.API
{
    public class EventSugestion
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<String>? Images { get; set; }
        public Participants? Participants { get; set; }

    }
}
