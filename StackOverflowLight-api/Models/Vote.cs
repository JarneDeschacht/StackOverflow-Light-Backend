using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.Models
{
    public class Vote
    {
        public int VoteId { get; set; }
        public User User { get; set; }
        public VoteType VoteType { get; set; }
        public DateTime CreationTime { get; set; }

        public Vote(User user,VoteType type)
        {
            User = user;
            VoteType = type;
            CreationTime = DateTime.UtcNow;
        }
        public Vote()
        {
            CreationTime = DateTime.UtcNow;
        }
    }
}
