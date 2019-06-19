using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public User Owner { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public int Score => Votes.Where(v => v.VoteType == VoteType.Upvote).Count()
            - Votes.Where(v => v.VoteType == VoteType.Downvote).Count();
        public DateTime CreationTime { get; set; }

        public Post(string title,string body,User owner)
        {
            Title = title;
            Body = body;
            Owner = owner;
            Votes = new List<Vote>();
            Answers = new List<Answer>();
            CreationTime = DateTime.UtcNow;
        }
        public Post()
        {
            Votes = new List<Vote>();
            Answers = new List<Answer>();
            CreationTime = DateTime.UtcNow;
        }
        public void AddVote(Vote vote)
        {
            Votes.Add(vote);
        }
        public void AddAnswer(Answer answer)
        {
            Answers.Add(answer);
        }
    }
}
