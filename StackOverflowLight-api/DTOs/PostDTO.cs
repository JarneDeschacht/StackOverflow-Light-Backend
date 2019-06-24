using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.DTOs
{
    public class PostDTO
    {
        [Required,MaxLength(80)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public int OwnerId { get; set; }
    }
}
