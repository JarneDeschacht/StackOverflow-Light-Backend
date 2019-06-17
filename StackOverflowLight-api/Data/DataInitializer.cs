using StackOverflowLight_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.Data
{
    public class DataInitializer
    {
        private readonly StackOverflowContext _dbContext;

        public DataInitializer(StackOverflowContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                User jarne = new User("Jarne", "Deschacht", "jarne.deschacht@hotmail.com");
                _dbContext.Users_Domain.Add(jarne);
                User ime = new User("Ime", "Van Daele", "imevandaele@gmail.com");
                _dbContext.Users_Domain.Add(ime);
                User tijs = new User("Tijs", "Martens", "tijs.martens@gmail.com");
                _dbContext.Users_Domain.Add(tijs);
                User robbe = new User("Robbe", "Dekien", "robbedekien@gmail.com");
                _dbContext.Users_Domain.Add(robbe);
                _dbContext.SaveChanges();
            }
        }
    }
}
