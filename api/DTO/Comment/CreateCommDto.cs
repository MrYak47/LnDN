using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO
{
    public class CreateCommDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be longer than 5 characters")]
        [MaxLength(280, ErrorMessage ="Title mustn't be over 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be longer than 5 characters")]
        [MaxLength(400, ErrorMessage ="Title mustn't be over 400 characters")]
        public string Content  { get; set; } =string.Empty;
    }
}