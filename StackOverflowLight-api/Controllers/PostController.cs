using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackOverflowLight_api.DTOs;
using StackOverflowLight_api.Models;

namespace StackOverflowLight_api.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostController(IPostRepository postRepository,IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }
        [HttpGet("{id}")]
        public ActionResult<Post> GetById(int id)
        {
            var post = _postRepository.GetById(id);
            if (post == null)
                return NotFound();
            return post;
        }
        [Authorize]
        [HttpPost]
        public ActionResult<Post> AddPost(PostDTO post)
        {
            var owner = _userRepository.GetById(post.OwnerId);

            if (owner == null)
                return NotFound();

            Post newpost = new Post(post.Title,post.Body,owner);
            _postRepository.Addpost(newpost);
            _postRepository.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = newpost.PostId }, newpost);
        }
        [Authorize]
        [HttpPost("{id}/votes")]
        public ActionResult<Vote> AddVote(int id, VoteDTO vote)
        {
            var user = _userRepository.GetById(vote.UserId);

            if (user == null)
                return NotFound();

            Vote newVote = new Vote(user,vote.VoteType);

            _postRepository.AddVote(id, newVote);
            _postRepository.SaveChanges();

            return Ok(newVote);
        }
        [Authorize]
        [HttpPost("{id}/answers")]
        public ActionResult<Answer> AddAnswer(int id, AnswerDTO answer)
        {
            var user = _userRepository.GetById(answer.userId);

            if (user == null)
                return NotFound();

            Answer newAnswer = new Answer(answer.Body,user);

            _postRepository.AddAnswer(id,newAnswer);
            _postRepository.SaveChanges();

            return Ok(newAnswer);
        }
        [Authorize]
        [HttpPost("{id}/deletevotes/{userid}")]
        public ActionResult<Vote> RemoveVote(int id, int userid)
        {
            _postRepository.RemoveVote(id, userid);
            _postRepository.SaveChanges();

            return Ok(_postRepository.GetById(id));
        }
    }
}