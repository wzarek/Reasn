namespace ReasnAPI.Models.API
{
    public class Participants(int participating, int interested)
    {
        public int Interested { get; set; } = interested;
        public int Participating { get; set; } = participating;
    }
}
