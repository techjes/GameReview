using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Models
{
    public class Review
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public bool Recommended { get; set; }
        public int HelpfulCount { get; set; }
        public int NotHelpfulCount { get; set; }
        public int GameId { get; set; }
    }
}
