using S2021A3AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A3AS.Controllers
{
    public class TrackController : Controller
    {
        // Reference to a manager object
        private Manager m = new Manager();
        // GET: Track
        public ActionResult Index()
        {
            return View(m.TrackGetAllWithDetail());
        }

        // GET: Track/Details/1
        public ActionResult Details(int id)
        {
            var track = m.TrackGetById(id);
            ViewBag.Title = track.Name + " Details";
            return View(track);
        }

        // GET: Track/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Create Track";
            ViewBag.Description = "Complete the form, and click the Create button";
            return View(new TrackAddFormViewModel() { 
                Milliseconds = 0, 
                UnitPrice = 0.0M, 
                AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title"),
                MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name")});
        }

        // POST: Track/Create
        [HttpPost]
        public ActionResult Create(TrackAddViewModel newTrack)
        {
            // Validate the input
            if (!ModelState.IsValid)
                return View(newTrack);

            try
            {
                // Process the input
                var addedItem = m.TrackAdd(newTrack);

                // If the item was not added, return the user to the Create page
                // otherwise redirect them to the Details page.
                if (addedItem == null)
                    return View(newTrack);
                else
                {
                    return RedirectToAction("Details", new { id = addedItem.TrackId });
                }
            }
            catch
            {
                return View(newTrack);
            }
        }
    }
}