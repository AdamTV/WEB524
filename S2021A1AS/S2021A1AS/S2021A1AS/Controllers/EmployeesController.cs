using S2021A1AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A1AS.Controllers
{
    public class EmployeesController : Controller
    {
        // Reference to a manager object
        private Manager m = new Manager();

        // GET: Employees
        public ActionResult Index()
        {
            return View(m.EmployeeGetAll());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var obj = m.EmployeeGetById(id.GetValueOrDefault());

            if (obj == null)
                return HttpNotFound();
            else
            {
                ViewBag.Title = $"View employee details - {obj.FirstName} {obj.LastName}";
                return View(obj);
            }
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            // Optionally create and send an object to the view
            return View(new EmployeeAddViewModel());
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(EmployeeAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
                return View(newItem);

            try
            {
                // Process the input
                var addedItem = m.EmployeeAdd(newItem);

                // If the item was not added, return the user to the Create page
                // otherwise redirect them to the Details page.
                if (addedItem == null)
                    return View(newItem);
                else
                {
                    ViewBag.Title = $"View employee details - {addedItem.FirstName} {addedItem.LastName}";
                    return RedirectToAction("Details", new { id = addedItem.EmployeeId });
                }
            }
            catch
            {
                return View(newItem);
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object
            var obj = m.EmployeeGetById(id.GetValueOrDefault());
            if (obj == null)
                return HttpNotFound();
            else
            {
                // Create and configure an "edit form"
                // Notice that obj is a CustomerBaseViewModel object so
                // we must map it to a CustomerEditContactFormViewModel object
                // Notice that we can use AutoMapper anywhere,
                // and not just in the Manager class.
                var formObj = m.mapper.Map<EmployeeBaseViewModel, EmployeeEditViewModel>(obj);
                ViewBag.Title = $"Edit employee information - {obj.FirstName} {obj.LastName}";
                return View(formObj);
            }
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EmployeeEditViewModel model)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Edit", new { id = model.EmployeeId });
            }
            if (id != model.EmployeeId)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("Index");
            }
            // Attempt to do the update
            var editedItem = m.EmployeeEdit(model);
            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Edit", new { id = model.EmployeeId });
            }
            else
            {
                // Show the details view, which will show the updated data
                return RedirectToAction("Details", new { id = model.EmployeeId });
            }
        }
    }
}