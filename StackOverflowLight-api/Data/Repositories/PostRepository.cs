using Microsoft.EntityFrameworkCore;
using StackOverflowLight_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly StackOverflowContext _context;
        private readonly DbSet<Post> _posts;

        public PostRepository(StackOverflowContext dbContext)
        {
            _context = dbContext;
            _posts = dbContext.Posts;
        }
        public void AddAnswer(int postid, Answer answer)
        {
            GetById(postid).AddAnswer(answer);
        }

        public void Addpost(Post post)
        {
            _posts.Add(post);
        }

        public void AddVote(int postid, Vote vote)
        {
            GetById(postid).AddVote(vote);
        }
        public void RemoveVote(int postid, int user)
        {
            GetById(postid).RemoveVote(user);
        }

        public IEnumerable<Post> GetAll()
        {
            return _posts
                .Include(p => p.Owner)
                .Include(p => p.Votes)
                .Include(p => p.Answers)
                .OrderByDescending(p => p.Score)
                .ThenBy(p => p.CreationTime)
                .ToList();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Post GetById(int id)
        {
            return _posts
                .Include(p => p.Owner)
                .Include(p => p.Votes).ThenInclude(v => v.User)
                .Include(p => p.Answers)
                .SingleOrDefault(p => p.PostId == id);
        }
    }
}
