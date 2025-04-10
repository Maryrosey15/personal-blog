using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PersonalBlogApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalBlogApi.Controllers
{
    [Route("blog/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IMongoCollection<BlogPost> _blogPosts;

        public BlogController(IMongoDatabase database)
        {
            _blogPosts = database.GetCollection<BlogPost>("BlogPosts");
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogPost>>> GetAll()
        {
            var posts = await _blogPosts.Find(post => true).ToListAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> Get(string id)
        {
            var post = await _blogPosts.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<BlogPost>> Create(BlogPost blogPost)
        {
            await _blogPosts.InsertOneAsync(blogPost);
            return CreatedAtAction(nameof(Get), new { id = blogPost.Id }, blogPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, BlogPost updatedPost)
        {
            var result = await _blogPosts.ReplaceOneAsync(p => p.Id == id, updatedPost);
            if (result.MatchedCount == 0) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _blogPosts.DeleteOneAsync(p => p.Id == id);
            if (result.DeletedCount == 0) return NotFound();
            return NoContent();
        }
    }
}