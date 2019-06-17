using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public DataInitializer(StackOverflowContext dbcontext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbcontext;
            _userManager = userManager;
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

                await CreateUser(jarne.Email, "P@ssword1111");
                await CreateUser(ime.Email, "P@ssword1111");
                await CreateUser(tijs.Email, "P@ssword1111");
                await CreateUser(robbe.Email, "P@ssword1111");

                _dbContext.SaveChanges();
            }
        }
        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }
    }
}
