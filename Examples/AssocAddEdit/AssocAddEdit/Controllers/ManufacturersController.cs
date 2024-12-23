using AssocAddEdit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocAddEdit.Controllers
{
    public class ManufacturersController : Controller
    {
        // Reference to the data manager
        private Manager m = new Manager();

        // GET: Manufacturers
        public ActionResult Index()
        {
            return View(m.ManufacturerGetAllWithDetail());
        }

        // GET: Manufacturers/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var manufacturer = m.ManufacturerGetByIdWithDetail(id.GetValueOrDefault());

            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(manufacturer);
            }
        }

        // GET: Manufacturers/5/AddVehicle
        // TODO: 17 - Used "attribute routing" for a custom URL segment (resource)
        [Route("manufacturers/{id}/addvehicle")]
        public ActionResult AddVehicle(int id)
        {
            // Attempt to get the associated object
            var manufacturer = m.ManufacturerGetById(id);

            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            else
            {
                // TODO: 18 - Add vehicle for a known manufacturer
                // We send the manufacturer identifier to the form
                // There, it is hidden... <input type=hidden
                // We also pass on the name, so that the browser user
                // knows which manufacturer they're working with

                // Create and configure a form object
                var formModel = new VehicleAddFormViewModel();
                formModel.ManufacturerId = manufacturer.Id;
                formModel.ManufacturerName = manufacturer.Name;

                IEnumerable<string> classifications = m.ClassificationGetAll().Select(m => m.Name);
                formModel.ClassificationList = new SelectList(classifications);

                return View(formModel);
            }
        }

        // POST: Manufacturers/5/AddVehicle
        // TODO: 19 - Used "attribute routing" for a custom URL segment (resource)
        [Route("manufacturers/{id}/addvehicle")]
        [HttpPost]
        public ActionResult AddVehicle(VehicleAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.VehicleAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                // TODO: 20 - Must redirect to the Vehicles controller
                return RedirectToAction("details", "vehicles", new { id = addedItem.Id });
            }
        }

        /*
        // GET: Manufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manufacturers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manufacturers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manufacturers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manufacturers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        */
    }
}
