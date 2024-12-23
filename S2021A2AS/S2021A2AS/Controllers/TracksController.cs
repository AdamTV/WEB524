using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A2AS.Controllers
{
    public class TracksController : Controller
    {
        Manager m = new Manager();

        // GET: Tracks/Index
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: Tracks/Blues
        public ActionResult Blues()
        {
            return View("Index", m.TrackGetAllBlues());
        }

        // GET: Tracks/MikePatton
        public ActionResult MikePatton()
        {
            return View("Index", m.TrackGetAllMikePatton());
        }

        // GET: Tracks/Top50Longest
        public ActionResult Top50Longest()
        {
            return View("Index", m.TrackGetAllTop50Longest());
        }

        // GET: Tracks/Top50Shortest
        public ActionResult Top50Shortest()
        {
            return View("Index", m.TrackGetAllTop50Shortest());
        }


    }
}