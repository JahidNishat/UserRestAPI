namespace UserAPI.Model
{
    public class TagViewModel
    {
        public long Id { get; set; }
        public List<string> Tags { get; set; }
        public int Expiry { get; set; }
    }
}
