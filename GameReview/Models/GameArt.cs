using System;


namespace GameReview.Models
{
    public class GameArt
    {
        private const string BASEURL = "http://thegamesdb.net/banners/";
        private string _thumbURL;
        private string _url;

        public int OriginalWidth { get; set; }
        public int OriginalHeight { get; set; }
        public string URL
        {
            get { return BASEURL + _url; }
            set { _url = value; }
        }
        public string ThumbURL
        {
            get { return BASEURL + _thumbURL; }
            set { _thumbURL = value; }
        }


    }
}
