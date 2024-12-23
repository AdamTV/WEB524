using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using S2021A5AS.EntityModels;
using S2021A5AS.Models;

namespace S2021A5AS.Controllers
{
    [Authorize]
    public class AlbumsController : Controller
    {
        private Manager m = new Manager();

        // GET: Albums
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var album = m.AlbumGetByIdWithDetail(id.Value);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //// GET: Albums/Create
        //[Authorize(Roles = "Clerk")]
        //public ActionResult Create()
        //{
        //    return View(new AlbumAddFormViewModel
        //    {
                
        //    });
        //}

        //// POST: Albums/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Clerk")]
        //public ActionResult Create(AlbumAddViewModel album)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    db.Albums.Add(album);
        //    //    db.SaveChanges();
        //    //    return RedirectToAction("Index");
        //    //}

        //    return View(album);
        //}

        [Route("albums/{id}/addtrack")]
        [Authorize(Roles = "Clerk")]
        public ActionResult AddTrack(int? id)
        {
            // Attempt to get the associated object
            var album = m.AlbumGetByIdWithDetail(id.Value);

            if (album == null)
            {
                return HttpNotFound();
            }
            else
            {
                var formModel = new TrackAddFormViewModel();
                formModel.AlbumName = album.Name;
                formModel.AlbumId = album.Id;

                IEnumerable<string> genres = m.GenreGetAll().Select(m => m.Name);
                formModel.GenreList = new SelectList(genres);

                return View(formModel);
            }

        }

        [Route("albums/{id}/addtrack")]
        [Authorize(Roles = "Clerk")]
        [HttpPost]
        public ActionResult AddTrack(TrackAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.TrackAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", "Tracks", new { id = addedItem.Id });
            }
        }
    }
}
