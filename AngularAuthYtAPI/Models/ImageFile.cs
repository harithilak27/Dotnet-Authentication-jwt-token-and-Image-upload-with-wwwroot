namespace Nethi.Models
{
    public class ImageFile
    {
        public string? title { get; set; }
        public IFormFile image { get; set; }
        public string? description { get; set; }
    }
}
