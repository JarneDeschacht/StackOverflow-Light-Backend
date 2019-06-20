using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.Models
{
    public interface IPostRepository
    {
        void Addpost(Post post);
        void AddVote(int postid, Vote vote);
        void RemoveVote(int postid, int user);
        void AddAnswer(int postid, Answer answer);
        IEnumerable<Post> GetAll();
        Post GetById(int id);
        void SaveChanges();
    }
}
