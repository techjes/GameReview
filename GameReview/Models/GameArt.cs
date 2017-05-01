using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameReview.Models
{
    public class GameArt
    {
        public enum ArtType
        {
            Banner,
            Box,
            Fan,
            ScreenShot
        }

        public int ID { get; set; }
        public int OriginalWidth { get; set; }
        public int OriginalHeight { get; set; }
        public string URL { get; set; }
        public string ThumbURL { get; set; }
        public ArtType Type { get; set; }
        [NotMapped]
        public string FullURL => "http://thegamesdb.net/banners/" + URL;
        [NotMapped]
        public string FullThumb => "http://thegamesdb.net/banners/" + ThumbURL;
    }
}
