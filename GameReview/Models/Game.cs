using System;
using System.Collections.Generic;

namespace GameReview.Models
{
    public class Game
    {
        public int ID { get; set; }
        public string GameTitle { get; set; }
        public string Platform { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Overview { get; set; }
        public string ESRB { get; set; }
        public List<string> Genres { get; set; }
        public int Players { get; set; }
        public bool CoOp { get; set; }
        public string Youtube { get; set; }
        public string Publisher { get; set; }
        public double Rating { get; set; }
        public List<GameArt> FanArt { get; set; }
        public List<GameArt> Banner { get; set; }      
        public List<GameArt> BoxArt { get; set; }
        public List<GameArt> Screenshots { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
