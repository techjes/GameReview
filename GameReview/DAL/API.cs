using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameReview.Models;
using System.Net;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace GameReview.DAL
{
    public static class API
    {
        private const string _base = "http://thegamesdb.net/api/";
        
        public static IEnumerable<Game> Search(string query)
        {
            string url = _base + "GetGamesList.php?name=";
            XDocument doc;
            IEnumerable<Game> results =null;
            
            
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + query);
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                doc = XDocument.Load(response.GetResponseStream());
                results = ParseSearch(doc);
            
            

            return results;
        }

        private static List<Game> ParseSearch(XDocument doc)
        {
            List<Game> results = new List<Game>();

            var data = doc.Elements(XName.Get("Data"));
            var games = data.Elements(XName.Get("Game"));

            foreach (var game in games)
            {
                DateTime time;
                Game newGame = new Game();
                DateTime.TryParse(game.Element(XName.Get("ReleaseDate")).Value, out time);

                newGame.ApiID = int.Parse(game.Element(XName.Get("id")).Value);
                newGame.GameTitle = game.Element(XName.Get("GameTitle")).Value;
                newGame.Platform = game.Element(XName.Get("Platform")).Value;

                if (time < DateTime.Parse("01/01/1900"))
                    newGame.ReleaseDate = null;
                else
                    newGame.ReleaseDate = time;
                
                results.Add(newGame);
            }
            return results;
        }

        public static Game GetDetails(int game)
        {
            string url = _base + "GetGame.php?id=";
            Game detailedGame = new Game();
            XDocument doc;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + game);
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                doc = XDocument.Load(response.GetResponseStream());
                
                detailedGame = ParseDetails(doc, detailedGame);
            }
            catch (Exception)
            {
                
            }

            return detailedGame;
        }

        private static Game ParseDetails(XDocument doc, Game game)
        {            
            var data = doc.Element(XName.Get("Data"));
            var games = data.Element(XName.Get("Game"));
            DateTime time;
            DateTime.TryParse(games.Element(XName.Get("ReleaseDate")).Value, out time);
            if (time < DateTime.Parse("01/01/1900"))
                game.ReleaseDate = null;
            else                        
                game.ReleaseDate = time;
            

            game.ApiID = int.Parse(games.Element(XName.Get("id")).Value);
            game.Overview = games.Element(XName.Get("Overview")).Value;
            game.ESRB = games.Element(XName.Get("ESRB")).Value;
            game.Genres = GetGenres(games);
            game.Players = int.Parse(games.Element(XName.Get("Players")).Value);
            game.CoOp = games.Element(XName.Get("Co-op")).Value == "No" ? false : true;
            game.Youtube = GetYoutubeID(games.Element(XName.Get("Youtube")).Value);
            game.Publisher = games.Element(XName.Get("Publisher")).Value;
            game.Rating = double.Parse(games.Element(XName.Get("Rating")).Value);
            game.ArtCollection = GetFanArt(games);
            game.GameTitle = games.Element(XName.Get("GameTitle")).Value;
            game.Platform = games.Element(XName.Get("Platform")).Value;

            return game;
        }

        private static string GetYoutubeID(string url)
        {
            var youtubeMatch = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)").Match(url);
            var embedURL = "https://www.youtube.com/embed/";

            if (youtubeMatch.Success)            
                return embedURL + youtubeMatch.Groups[1];
            
            else
                return string.Empty;
        }

        private static List<string> GetGenres(XElement doc)
        {
            List<string> genres = new List<string>(); 
            var genreElements = doc.Elements(XName.Get("genre"));

            foreach (var genre in genreElements)
            {
                genres.Add(genre.Value);
            }

            return genres;
        }

        private static List<GameArt> GetFanArt(XElement doc)
        {
            List<GameArt> art = new List<GameArt>();
            var image = doc.Element(XName.Get("Images"));
            var items = image.Elements(XName.Get("fanart"));

            foreach (var item in items)
            {
                var original = item.Element(XName.Get("original"));
                var thumb = item.Element(XName.Get("thumb"));
                art.Add(new GameArt()
                {
                    OriginalWidth = int.Parse(original.Attribute(XName.Get("width")).Value),
                    OriginalHeight = int.Parse(original.Attribute(XName.Get("height")).Value),
                    URL = original.Value,
                    ThumbURL = thumb.Value,
                    Type = GameArt.ArtType.Fan
                });
            }
            art.AddRange(GetBoxArt(doc));
            art.AddRange(GetBanners(doc));
            art.AddRange(GetScreenshots(doc));

            return art;
        }

        private static List<GameArt> GetBoxArt(XElement doc)
        {
            List<GameArt> art = new List<GameArt>();
            var image = doc.Element(XName.Get("Images"));

            var items = image.Elements(XName.Get("boxart"));

            foreach (var item in items)
            {
                art.Add(new GameArt()
                {
                    OriginalWidth = int.Parse(item.Attribute(XName.Get("width")).Value),
                    OriginalHeight = int.Parse(item.Attribute(XName.Get("height")).Value),
                    URL = item.Value,
                    ThumbURL = item.Attribute(XName.Get("thumb")).Value,
                    Type = GameArt.ArtType.Box
                });
            }
            return art;
        }

        private static List<GameArt> GetBanners(XElement doc)
        {
            List<GameArt> art = new List<GameArt>();
            var image = doc.Element(XName.Get("Images"));

            var items = image.Elements(XName.Get("banner"));

            foreach (var item in items)
            {
                art.Add(new GameArt()
                {
                    OriginalWidth = int.Parse(item.Attribute(XName.Get("width")).Value),
                    OriginalHeight = int.Parse(item.Attribute(XName.Get("height")).Value),
                    URL = item.Value,
                    ThumbURL = null,
                    Type = GameArt.ArtType.Banner
                });
            }
            return art;
        }

        private static List<GameArt> GetScreenshots(XElement doc)
        {
            List<GameArt> art = new List<GameArt>();
            var image = doc.Element(XName.Get("Images"));

            var items = image.Elements(XName.Get("screenshot"));

            foreach (var item in items)
            {
                var original = item.Element(XName.Get("original"));
                var thumb = item.Element(XName.Get("thumb"));
                art.Add(new GameArt()
                {
                    OriginalWidth = int.Parse(original.Attribute(XName.Get("width")).Value),
                    OriginalHeight = int.Parse(original.Attribute(XName.Get("height")).Value),
                    URL = original.Value,
                    ThumbURL = thumb.Value,
                    Type = GameArt.ArtType.ScreenShot
                });
            }
            return art;
        }
    }
}