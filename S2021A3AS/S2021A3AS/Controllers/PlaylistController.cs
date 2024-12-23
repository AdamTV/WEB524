using S2021A3AS.EntityModels;
using S2021A3AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A3AS.Controllers
{
    public class PlaylistController : Controller
    {
        // Reference to a manager object
        private Manager m = new Manager();
        // GET: Playlist
        public ActionResult Index()
        {
            return View(m.PlaylistGetAll());
        }
        // GET: Playlist/Details/5
        public ActionResult Details(int id)
        {
            var obj = m.PlaylistGetById(id);
            ViewBag.Description = obj.Name;
            return View(obj);
        }

        // GET: Playlist/Edit/5
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object
            var obj = m.PlaylistGetById(id.GetValueOrDefault());
            if (obj == null)
                return HttpNotFound();
            else
            {
                // Create and configure an "edit form"
                // Notice that obj is a CustomerBaseViewModel object so
                // we must map it to a CustomerEditContactFormViewModel object
                // Notice that we can use AutoMapper anywhere,
                // and not just in the Manager class.
                var formObj = m.mapper.Map<PlaylistBaseViewModel, PlaylistEditTracksFormViewModel>(obj);
                ViewBag.Description = formObj.Name;
                formObj.AllTracksList = new SelectList(m.TrackGetAllWithDetail(), "TrackId", "NameFull");
                formObj.TracksOnPlaylist = m.mapper.Map<ICollection<Track>, ICollection<TrackBaseViewModel>>(obj.Tracks);
                return View(formObj);
            }
        }

        // POST: Playlist/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PlaylistEditTracksViewModel newData)
        {
            var result = m.PlaylistEditTracks(id, newData);
            if (result != null)
                return RedirectToAction("Details", new { id = id });
            else
                return RedirectToAction("Edit", new { id = id });
        }
    }
}