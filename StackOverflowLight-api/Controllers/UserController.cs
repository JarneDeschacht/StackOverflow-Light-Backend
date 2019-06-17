using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackOverflowLight_api.Models;

namespace StackOverflowLight_api.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }
        [HttpGet("{email}")]
        public ActionResult<User> GetByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null)
                return NotFound();
            return user;
        }
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            if (user == null)
                return NotFound();

            _userRepository.Add(user);
            _userRepository.SaveChanges();
            return user;
        }
        [HttpPut("{id}")]
        public ActionResult<User> PutUser(int id, User user)
        {
            if (id != user.UserId)
                return BadRequest();

            var usr = _userRepository.Update(user);
            _userRepository.SaveChanges();
            return usr;
        }
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                return NotFound();

            _userRepository.Delete(user);
            _userRepository.SaveChanges();
            return user;
        }
    }
}
