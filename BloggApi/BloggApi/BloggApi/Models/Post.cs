using MongoDB.Bson;

namespace BloggApi.Models
{
    public class Post
    {
        public ObjectId Id { get; set; }

        public string Title { get; set; }   

        public string Summary { get; set; } 

        public string Maincontent { get; set; }

        public int Mood { get; set; }
    }
}
