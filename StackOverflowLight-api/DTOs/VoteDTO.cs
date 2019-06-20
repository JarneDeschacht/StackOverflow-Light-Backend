using StackOverflowLight_api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.DTOs
{
    public class VoteDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public VoteType VoteType { get; set; }
    }
}
