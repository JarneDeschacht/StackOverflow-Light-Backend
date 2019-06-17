using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.Models
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetByEmail(string email);
        User GetById(int id);
        void Add(User user);
        void Delete(User user);
        User Update(User user);
        void SaveChanges();
    }
}
