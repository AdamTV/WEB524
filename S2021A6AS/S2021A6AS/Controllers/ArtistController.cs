using S2021A6AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace S2021A6AS.Controllers
{
    [Authorize]
    public class ArtistController : Controller
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
            var obj = m.ArtistGetByIdWithMediaItemInfo(id.Value);
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
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Executive")]
        public ActionResult Create(ArtistAddViewModel newArtist)
        {
            // Validate the input
            if (!ModelState.IsValid)
                return View(newArtist);

            var len = newArtist.Biography.Length;

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

        [Route("artist/{id}/addalbum")]
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
                ViewBag.ArtistName = artist.Name;
                ViewBag.currentArtistId = artist.Id;
                return View(new AlbumAddFormViewModel()
                {
                    GenreList = new SelectList(m.GenreGetAll().Select(m => m.Name))
                });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("artist/{id}/addalbum")]
        [Authorize(Roles = "Coordinator")]
        public ActionResult AddAlbum(AlbumAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.AlbumAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", "Album", new { id = addedItem.Id });
            }
        }


        [Route("artist/{id}/addmediaitem")]
        [Authorize(Roles = "Coordinator")]
        public ActionResult AddMediaItem(int? id)
        {
            // Attempt to get the associated object
            var artist = m.ArtistGetByIdWithDetail(id.Value);

            if (artist == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.ArtistName = artist.Name;
                return View(new ArtistMediaItemAddFormViewModel()
                {
                    ArtistId = artist.Id
                });
            }
        }

        [HttpPost]
        [Route("artist/{id}/addmediaitem")]
        [Authorize(Roles = "Coordinator")]
        public ActionResult AddMediaItem(ArtistMediaItemAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.ArtistMediaItemAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", "Artist", new { id = addedItem.Id });
            }
        }
    }
}
