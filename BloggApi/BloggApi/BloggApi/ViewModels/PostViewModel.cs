using BloggApi.Models;

namespace BloggApi.ViewModels
{
    public class PostViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string Summary { get; set; }

        public string Maincontent { get; set; } 

        public PostViewModel(Post post)
        {
            Id = post.Id.ToString();
            Title = post.Title;
            Summary = post.Summary;
            Maincontent = post.Maincontent;
        }
    }
}
