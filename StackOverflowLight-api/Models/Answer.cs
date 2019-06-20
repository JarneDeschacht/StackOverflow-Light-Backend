using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string Body { get; set; }
        public User User { get; set; }
        public DateTime CreationTime { get; set; }

        public Answer(string body,User user)
        {
            Body = body;
            User = user;
            CreationTime = DateTime.Now;
        }
        public Answer()
        {
            CreationTime = DateTime.Now;
        }
    }
}
