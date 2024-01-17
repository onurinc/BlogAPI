using Microsoft.AspNetCore.Mvc;
using BlogAPI.Core;
using BlogAPI.Models;
using BlogAPI.Services;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogsController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageProducer _messageProducer;

        public BlogsController(
            IUnitOfWork unitOfWork, 
            IMessageProducer messageProducer)
        {
            _unitOfWork = unitOfWork;
            _messageProducer = messageProducer;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.Blogs.All());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var blog = await _unitOfWork.Blogs.GetById(id);
            if (blog == null) return NotFound();

            return Ok(blog);
        }

        [HttpPost]
        public async Task<IActionResult> AddBlog(Blog blog)
        {
            _unitOfWork.Blogs.Add(blog);
            await _unitOfWork.CompleteAsync();

            //_messageProducer.SendingMessage<Blog>(blog);

            return Ok();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await _unitOfWork.Blogs.GetById(id);

            if (blog == null) return NotFound();

            await _unitOfWork.Blogs.Delete(blog);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlog(Blog blog)
        {
            var existBlog = await _unitOfWork.Blogs.GetById(blog.Id);
            if (existBlog == null) return NotFound();

            await _unitOfWork.Blogs.Update(blog);
            await _unitOfWork.CompleteAsync();


            return NoContent();
        }
    }
}