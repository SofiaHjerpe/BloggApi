using BloggApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloggApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet(Name = "GetPosts")]
        public async Task<IEnumerable<PostViewModel>> Get()
        {
            var db = new Database();
            var posts = await db.GetPosts();

            var viewModel = new List<PostViewModel>();
            foreach (var post in posts)
            {
                viewModel.Add(new PostViewModel(post));
            }
            return viewModel;
        }
        [HttpGet("{id}", Name = "GetPost")]
        public async Task<PostViewModel> GetById(string id)
        {
            var db = new Database();
            var post = await db.GetPost(id);

            var viewModel = new PostViewModel(post);

            return viewModel;
        }
        [HttpPost(Name = "PostBlogg")]
        public async Task<IActionResult> Post(string title, string summary, string maincontent, int mood)
        {
            var db = new Database();

            if (title.Length > 100 || summary.Length > 150 || maincontent.Length > 5000)
            {
                return BadRequest("the title or summary or maincontent is in the wrong format ... ");
            }
            else
            {
                await db.SavePost(title, summary, maincontent, mood);
                return Ok();
            }

        }

        [HttpDelete("{id}", Name = "DeletePost")]
        public async Task<IActionResult> DeleteById(string id)
        {
            var db = new Database();
            await db.DeletePost(id);
            return Ok();
        }
        [HttpPut("{id}", Name = "PutPost")]
        public async Task<IActionResult> PutPost(string id, string title, string summary, string maincontent)
        {
            var db = new Database();

            if (title.Length > 100 || summary.Length > 150 || maincontent.Length > 5000)
            {
                return BadRequest("the title or summary or maincontent is in the wrong format ... ");
            }
            else
            {
                await db.UpdatePost(id, title, summary, maincontent);
                return Ok();
            }
        }
}
}
