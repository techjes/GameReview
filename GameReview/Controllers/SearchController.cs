using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameReview.DAL;
using GameReview.Models;
namespace GameReview.Controllers
{
    public class SearchController : Controller
    {

        IEnumerable<Game> _games;
        
        
        [HttpGet]
        public ActionResult Index()
        {
            if (Request.QueryString["q"] != null)
            {
                _games = API.Search(Request.QueryString["q"]);
            }
            return View(_games);
        }

        
        public ActionResult Details(int id)
        {
            
            Game game = API.GetDetails(id);
            return View(game);
        }

        public ActionResult GameArt(int id)
        {
            var gameArt = _games.FirstOrDefault(x => x.ID == id).BoxArt;
            ViewBag.art = gameArt;
            return PartialView(gameArt);
        }
    }
}