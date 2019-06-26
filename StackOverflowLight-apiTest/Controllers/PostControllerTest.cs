using Microsoft.AspNetCore.Mvc;
using Moq;
using StackOverflowLight_api.Controllers;
using StackOverflowLight_api.DTOs;
using StackOverflowLight_api.Models;
using StackOverflowLight_apiTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace StackOverflowLight_apiTest.Controllers
{
    public class PostControllerTest
    {
        private readonly DummyDbContext _dummyContext;
        private PostController _postController;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IPostRepository> _mockPostRepository;
        private Post _post1;
        private User _jarne;

        public PostControllerTest()
        {
            _dummyContext = new DummyDbContext();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPostRepository = new Mock<IPostRepository>();
            _post1 = _dummyContext.Post1;
            _jarne = _dummyContext.Jarne;

            _mockUserRepository.Setup(c => c.GetAll()).Returns(_dummyContext.Users);
            _mockUserRepository.Setup(c => c.GetById(_jarne.UserId)).Returns(_jarne);

            _mockPostRepository.Setup(a => a.GetAll()).Returns(_dummyContext.Posts);
            _mockPostRepository.Setup(a => a.GetById(_post1.PostId)).Returns(_post1);

            _postController = new PostController(_mockPostRepository.Object,_mockUserRepository.Object){ };
        }

        [Fact]
        public void Post_GetAll_ReturnsAllPosts()
        {
            var result = _postController.GetAll();
            List<Post> posts = result.ToList();
            Assert.Equal(_dummyContext.Posts.Count(), posts.Count());
        }
        [Fact]
        public void Post_GetById_ExistingId_ReturnsPost()
        {
            var result = _postController.GetById(_post1.PostId);
            Assert.Equal(_post1, result.Value);
        }
        [Fact]
        public void Post_GetById_NonExistingId_ReturnsPost()
        {
            var result = _postController.GetById(999);
            Assert.Null(result.Value);
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public void Post_AddPost_ExistingOwner_AddsPost()
        {
            var newpost = new PostDTO() { Title = "Test", Body = "Test", OwnerId = _jarne.UserId };
            var result = _postController.AddPost(newpost);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
        [Fact]
        public void Post_AddPost_NonExistingOwner_ReturnsNotFound()
        {
            var newpost = new PostDTO() { Title = "Test", Body = "Test", OwnerId = 999 };
            var result = _postController.AddPost(newpost);
            Assert.Null(result.Value);
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public void Post_AddVote_ExistingUser_AddsVote()
        {
            var newvote = new VoteDTO() { UserId = _jarne.UserId, VoteType = VoteType.Upvote };
            var result = _postController.AddVote(_post1.PostId,newvote);
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public void Post_AddVote_ExistingNonUser_ReturnsNotFound()
        {
            var newvote = new VoteDTO() { UserId = 999, VoteType = VoteType.Upvote };
            var result = _postController.AddVote(_post1.PostId, newvote);
            Assert.Null(result.Value);
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public void Post_AddAnswer_ExistingUser_AddsAnswer()
        {
            var newanswer = new AnswerDTO() { Body = "Test",userId = _jarne.UserId};
            var result = _postController.AddAnswer(_post1.PostId,newanswer);
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public void Post_Answer_ExistingNonUser_ReturnsNotFound()
        {
            var newanswer = new AnswerDTO() { Body = "Test", userId = 999 };
            var result = _postController.AddAnswer(_post1.PostId, newanswer);
            Assert.Null(result.Value);
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public void Post_Vote_DeleteVote_DeletesVote()
        {
            var result = _postController.RemoveVote(_post1.PostId, _jarne.UserId);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
