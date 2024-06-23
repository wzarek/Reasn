using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.API
{
    public enum SortBy
    {
        CreatedAt,
        StartAt,
        Name
    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }

    public class PagedRequest
    {
        public string? FilterName { get; set; }
        public EventStatus? FilterStatus { get; set; }
        public List<string>? FilterTags { get; set; }
        public DateTime? FilterStartAt { get; set; }
        public DateTime? FilterEndAt { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public SortBy SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}

