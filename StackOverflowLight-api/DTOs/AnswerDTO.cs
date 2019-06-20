using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.DTOs
{
    public class AnswerDTO
    {
        [Required]
        public string Body { get; set; }
        [Required]
        public int userId { get; set; }
    }
}
