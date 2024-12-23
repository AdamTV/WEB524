using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A3AS.Controllers
{
    public class MediaTypeController : Controller
    {
        // Reference to a manager object
        private Manager m = new Manager();
        // GET: MediaType
        public ActionResult Index()
        {
            return View(m.MediaTypeGetAll());
        }
        // GET: MediaType/Details/5
        public ActionResult Details(int id)
        {
            var mediaType = m.MediaTypeGetById(id);
            ViewBag.Title = mediaType.Name + " Details";
            return View(mediaType);
        }
    }
}