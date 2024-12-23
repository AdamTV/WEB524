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
    public class TracksController : Controller
    {
        private Manager m = new Manager();

        // GET: Tracks
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: Tracks/Details/5
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

        //// GET: Tracks/Create
        //public ActionResult Create()
        //{
        //    return View();
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

        //// GET: Tracks/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Track track = db.Tracks.Find(id);
        //    if (track == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(track);
        //}

        //// POST: Tracks/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Clerk,Composers,Genre,Name")] Track track)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(track).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(track);
        //}

        //// GET: Tracks/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Track track = db.Tracks.Find(id);
        //    if (track == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(track);
        //}

        //// POST: Tracks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Track track = db.Tracks.Find(id);
        //    db.Tracks.Remove(track);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
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
