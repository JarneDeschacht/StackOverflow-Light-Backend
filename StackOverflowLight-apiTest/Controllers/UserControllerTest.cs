using Microsoft.AspNetCore.Mvc;
using Moq;
using StackOverflowLight_api.Controllers;
using StackOverflowLight_api.Models;
using StackOverflowLight_apiTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace StackOverflowLight_apiTest.Controllers
{
    public class UserControllerTest
    {
        private readonly DummyDbContext _dummyContext;
        private UserController _userController;
        private Mock<IUserRepository> _mockUserRepository;
        private User _jarne;
        private string _noneExistingEmail = "test@test.be";
        private User _newJarne;

        public UserControllerTest()
        {
            _dummyContext = new DummyDbContext();
            _mockUserRepository = new Mock<IUserRepository>();
            _jarne = _dummyContext.Jarne;
            _newJarne = _jarne;
            _newJarne.FirstName = "Bob";

            _mockUserRepository.Setup(c => c.GetAll()).Returns(_dummyContext.Users);
            _mockUserRepository.Setup(c => c.GetById(_jarne.UserId)).Returns(_jarne);
            _mockUserRepository.Setup(c => c.GetByEmail(_jarne.Email)).Returns(_jarne);
            _mockUserRepository.Setup(c => c.Update(_newJarne)).Returns(_newJarne);

            _userController = new UserController(_mockUserRepository.Object){};
        }

        [Fact]
        public void User_GetAll_returnsAllUsers()
        {
            var result = _userController.GetAll();
            List<User> users = result.ToList();
            Assert.Equal(_dummyContext.Users.Count(), users.Count());
        }
        [Fact]
        public void User_GetByEmail_ExistingEmail_returnsUser()
        {
            var result = _userController.GetByEmail(_jarne.Email);
            Assert.Equal(_jarne, result.Value);
        }
        [Fact]
        public void User_GetByEmail_NonExistingEmail_returnsNotFound()
        {
            var result = _userController.GetByEmail(_noneExistingEmail);
            Assert.Null(result.Value);
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public void User_PutUser_ExistingUserId_UpdatesUser()
        {
            var result = _userController.PutUser(_jarne.UserId,_newJarne);
            Assert.Equal(_newJarne,result.Value);
        }
        [Fact]
        public void User_PutUser_NonExistingid_returnsNotFound()
        {
            var result = _userController.PutUser(999,_newJarne);
            Assert.Null(result.Value);
            Assert.IsType<BadRequestResult>(result.Result);
        }
        [Fact]
        public void User_DeleteUser_ExistingUserId_DeletesAndReturnsUser()
        {
            var result = _userController.DeleteUser(_jarne.UserId);
            Assert.Equal(_jarne, result.Value);
        }
        [Fact]
        public void User_DeleteUser_NonExistingid_returnsNotFound()
        {
            var result = _userController.DeleteUser(999);
            Assert.Null(result.Value);
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
