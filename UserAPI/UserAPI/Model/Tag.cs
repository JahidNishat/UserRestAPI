using System.ComponentModel.DataAnnotations;

namespace UserAPI.Model
{
    public class Tag
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string TagName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Expiry { get; set; }
    }
}
