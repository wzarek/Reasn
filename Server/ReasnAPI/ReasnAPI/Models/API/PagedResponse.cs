using ReasnAPI.Models.Enums;

namespace ReasnAPI.Models.API
{
    public class PagedResponse<T>
    {
        public enum SortByEnum
        {
            CreatedAt,
            StartAt,
            Name
        }

        public enum SortOrderEnum
        {
            Ascending,
            Descending
        }

        public List<T> Items { get; set; }
        public string FilterName { get; set; }
        public EventStatus? FilterStatus { get; set; }
        public List<string> FilterTags { get; set; }
        public DateTime? FilterStartAt { get; set; }
        public DateTime? FilterEndAt { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public SortByEnum SortBy { get; set; }
        public SortOrderEnum SortOrder { get; set; }
        public int TotalCount { get; set; }
    }
}
