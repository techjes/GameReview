using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameReview.Models
{
    public class Game
    {
        public int ID { get; set; }
        public int ApiID { get; set; }
        [Display(Name = "Game Title")]
        public string GameTitle { get; set; }
        public string Platform { get; set; }
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? ReleaseDate { get; set; }
        public string Overview { get; set; }
        public string ESRB { get; set; }
        public List<string> Genres { get; set; }
        public int Players { get; set; }
        public bool CoOp { get; set; }
        public string Youtube { get; set; }
        public string Publisher { get; set; }
        public double Rating { get; set; }
        public virtual ICollection<GameArt> ArtCollection { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }       
        public bool Detailed { get; set; }
    }
}
