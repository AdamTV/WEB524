using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A3AS.Controllers
{
    public class AlbumController : Controller
    {
        // Reference to a manager object
        private Manager m = new Manager();
        // GET: Album
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }
        // GET: Album/5
        public ActionResult Details(int id)
        {
            var album = m.AlbumGetById(id);
            ViewBag.Title = album.Title + " Details";
            return View(album);
        }
    }
}