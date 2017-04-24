using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Models
{
    public class Review
    {
        public int ID { get; set; }
        [Required]
        public string Content { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DateCreated { get; set; }
        [Required]
        [Display(Name = "Do you recommend this game?")]
        public bool Recommended { get; set; }
        public int HelpfulCount { get; set; }
        public int NotHelpfulCount { get; set; }
        [Required]
        public int UserName { get; set; }
    }
}
