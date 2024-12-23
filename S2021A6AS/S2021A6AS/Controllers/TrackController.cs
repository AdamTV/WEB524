using S2021A6AS.EntityModels;
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
    public class TrackController : Controller
    {
        private Manager m = new Manager();

        // GET: Track
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }



        // GET: Photo/5
        // TODO 8 - Uses attribute routing
        [Route("track/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var track = m.TrackGetByIdWithDetail(id.Value);
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        // GET: Track/Details/5
        [Route("track/{id}/clip")]
        public ActionResult Clip(int? id)
        {
            // Attempt to get the matching object
            var o = m.TrackAudioClipGetById(id.GetValueOrDefault());

            if (o == null || o.AudioContentType == null)
            {
                return HttpNotFound();
            }
            else
            {
                // TODO 9 - Return a file content result
                // Set the Content-Type header, and return the photo bytes
                return File(o.Audio, o.AudioContentType);
            }        
        }

        //[Route(Name = "clip/{id}")]
        //public ActionResult GetAudioClipByTrackId(int? id)
        //{
        //    return View();
        //}

        //// GET: Tracks/Create
        //public ActionResult Create()
        //{
        //    return View(new TrackAddFormViewModel());
        //}

        //// POST: Tracks/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Clerk,Composers,Genre,Name")] Track track)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    db.Tracks.Add(track);
        //    //    db.SaveChanges();
        //    //    return RedirectToAction("Index");
        //    //}

        //    return View(track);
        //}

        // GET: Tracks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var obj = m.TrackGetByIdWithDetail(id.Value);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(new TrackUpdateClipFormViewModel()
            {
                TrackId = obj.Id,
                TrackName = obj.Name
            });
        }

        // POST: Tracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, TrackUpdateClipViewModel newData)
        {
            var result = m.TrackUpdateAudioClip(id, newData);
            if (result != null)
                return RedirectToAction("Details", new { id = id });
            else
                return RedirectToAction("Edit", new { id = id });
        }

        //// GET: Tracks/Delete/5
        //public ActionResult Delete(int? id)
        //{

        //}

        //// POST: Tracks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var result = m.TrackUpdateClip(id, newData);
        //    if (result != null)
        //        return RedirectToAction("Details", new { id = id });
        //    else
        //        return RedirectToAction("Edit", new { id = id });
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}