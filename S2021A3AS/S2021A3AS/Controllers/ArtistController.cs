using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A3AS.Controllers
{
    public class ArtistController : Controller
    {
        // Reference to a manager object
        private Manager m = new Manager();

        // GET: Artist
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artist/Details/5
        public ActionResult Details(int id)
        {
            var artist = m.ArtistGetById(id);
            ViewBag.Title = artist.Name + " Details";
            return View(artist);
        }
    }
}