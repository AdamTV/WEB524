using AssocAddEdit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocAddEdit.Controllers
{
    public class VehiclesController : Controller
    {
        // Reference to the data manager
        private Manager model = new Manager();

        // GET: Vehicles
        public ActionResult Index()
        {
            return View(model.VehicleGetAllWithDetail());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = model.VehicleGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }

        // TODO: 31 - Build the data for the HTML Form, for "add new" vehicle
        // GET: Vehicles/Create
        public ActionResult Create()
        {
            // Create a form
            var form = new VehicleAddFormViewModel();

            // Configure the SelectList for the item-selection element on the HTML Form
            form.ClassificationList = new SelectList(model.ClassificationGetAll(), "Name", "Name");

            // Configure the SelectList for the item-selection element on the HTML Form
            form.ManufacturerList = new SelectList(model.ManufacturerGetAll(), "Id", "Name");
            
            return View(form);
        }

        // POST: Vehicles/Create
        [HttpPost]
        public ActionResult Create(VehicleAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = model.VehicleAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.Id });
            }
        }

        // TODO: 32 - Build the data for the HTML Form, for "edit existing" vehicle
        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object
            var vehicle = model.VehicleGetByIdWithDetail(id.GetValueOrDefault());

            if (vehicle == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create and configure an "edit form"

                // Notice that o is a CustomerBase object
                // We must map it to a CustomerEditContactInfoForm object
                // Notice that we can use AutoMapper anywhere, 
                // and not just in the Manager class!
                var formModel = model.mapper.Map<VehicleEditFormViewModel>(vehicle);

                return View(formModel);
            }
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, VehicleEditViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.Id });
            }

            if (id.GetValueOrDefault() != newItem.Id)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            // Attempt to do the update
            var editedItem = model.VehicleEditMSRP(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.Id });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("details", new { id = newItem.Id });
            }
        }

        // TODO: 33 - Delete item (vehicle) pair of methods

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            var itemToDelete = model.VehicleGetByIdWithDetail(id.GetValueOrDefault());

            if (itemToDelete == null)
            {
                // Don't leak info about the delete attempt
                // Simply redirect
                return RedirectToAction("index");
            }
            else
            {
                return View(itemToDelete);
            }
        }

        // POST: Vehicles/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = model.VehicleDelete(id.GetValueOrDefault());

            // "result" will be true or false
            // We probably won't do much with the result, because 
            // we don't want to leak info about the delete attempt

            // In the end, we should just redirect to the list view
            return RedirectToAction("index");
        }
    }
}
