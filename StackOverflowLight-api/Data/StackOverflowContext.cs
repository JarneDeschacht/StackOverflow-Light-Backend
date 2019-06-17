using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackOverflowLight_api.Data.Mappers;
using StackOverflowLight_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.Data
{
    public class StackOverflowContext : IdentityDbContext
    {
        public DbSet<User> Users_Domain { get; set; }

        public StackOverflowContext(DbContextOptions<StackOverflowContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
