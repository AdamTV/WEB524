using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using S2021A5AS.EntityModels;
using S2021A5AS.Models;

namespace S2021A5AS.Controllers
{
    [Authorize]
    public class ArtistsController : Controller
    {
        private Manager m = new Manager();

        // GET: Artists
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var obj = m.ArtistGetByIdWithDetail(id.Value);
            obj.AlbumsCount = m.AlbumGetAllByArtistId(id.Value).Count();
            return View(obj);
        }

        // GET: Artists/Create
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            return View(new ArtistAddFormViewModel
            {
                GenreList = new SelectList(m.GenreGetAll(), "Id", "Name")
            });
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Executive")]
        public ActionResult Create(ArtistAddViewModel newArtist)
        {
            // Validate the input
            if (!ModelState.IsValid)
                return View(newArtist);

            try
            {
                //newArtist.Executive = "test";
                newArtist.Genre = m.GenreGetAll().Where(g => g.Id == Convert.ToInt32(newArtist.Genre)).FirstOrDefault().Name;
                //newArtist.Genre = 
                // Process the input
                var addedItem = m.ArtistAdd(newArtist);

                // If the item was not added, return the user to the Create page
                // otherwise redirect them to the Details page.
                if (addedItem == null)
                    return View(newArtist);
                else
                {
                    return RedirectToAction("Details", new { id = addedItem.Id });
                }
            }
            catch
            {
                return View(newArtist);
            }
        }

        [Route("artists/{id}/addalbum")]
        [Authorize(Roles = "Coordinator")]
        public ActionResult AddAlbum(int? id)
        {
            // Attempt to get the associated object
            var artist = m.ArtistGetByIdWithDetail(id.Value);

            if (artist == null)
            {
                return HttpNotFound();
            }
            else
            {               
                var formModel = new AlbumAddFormViewModel();
                formModel.ArtistName = artist.Name;

                IEnumerable<string> genres = m.GenreGetAll().Select(m => m.Name);
                formModel.GenreList = new SelectList(genres);
                formModel.ArtistList = new SelectList(m.ArtistGetAll(), "Id", "Name");
                formModel.TracksList = new SelectList(m.TrackGetAllByArtistId(artist.Id), "Id", "Name");
                ViewBag.currentArtistId = artist.Id;
                return View(formModel);
            }
        }

        [Route("artists/{id}/addalbum")]
        [Authorize(Roles = "Coordinator")]
        [HttpPost]
        public ActionResult AddAlbum(AlbumAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.AlbumAdd(newItem);
            newItem.Coordinator = addedItem.Coordinator;

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", "Albums", new { id = addedItem.Id });
            }
        }
    }
}
