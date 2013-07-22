namespace EloquaImageSample.Models
{
    public class ImageFile
    {
        public int? createdAt { get; set; }
        public int? createdBy { get; set; }
        public int? folderId { get; set; }
        public string fullImageUrl { get; set; }
        public int? id { get; set; }
        public string name { get; set; }
        public Size size { get; set; }
        public int? updatedAt { get; set; }
        public int? updatedBy { get; set; }
    }
}
