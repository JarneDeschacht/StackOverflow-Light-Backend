using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackOverflowLight_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StackOverflowContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(StackOverflowContext dbContext)
        {
            _context = dbContext;
            _users = dbContext.Users_Domain;
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }
        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email.Equals(email));
        }

        public User GetById(int id)
        {
            return _users.SingleOrDefault(u => u.UserId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public User Update(User user)
        {
            _context.Update(user);
            return GetById(user.UserId);
        }
    }
}
