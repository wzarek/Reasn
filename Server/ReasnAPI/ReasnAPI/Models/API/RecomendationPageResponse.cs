namespace ReasnAPI.Models.API
{
    public class RecomendationPageResponse<T>
    {
        public List<T>? Items { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
