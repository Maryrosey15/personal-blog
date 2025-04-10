using System;

namespace PersonalBlogApi.Models
{
    public class BlogPost
    {
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Author { get; set; }
    }
}