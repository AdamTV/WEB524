using DataAnnotations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataAnnotations.Controllers
{
    public class HomeController : Controller
    {
        // TODO: 06 - Nothing much going on here - data annotations are neutral to controllers
        // However, they help validate user-entered data BEFORE it comes into the controller!

        public ActionResult Index()
        {
            return View();
        }

        // ############################################################
        // Vehicle methods

        // Renders a "details" view
        public ActionResult DetailsVehicle(VehicleAddViewModel item)
        {
            return View(item);
        }

        // Renders a "details" view
        public ActionResult DetailsVehiclePlain(VehicleAddPlainViewModel item)
        {
            return View(item);
        }

        // Display an HTML Form for vehicle "add new" use case
        // With useful data annotations

        public ActionResult CreateVehicle()
        {
            var form = new VehicleAddViewModel();

            return View(form);
        }

        [HttpPost]
        public ActionResult CreateVehicle(VehicleAddViewModel newItem)
        {
            return View("detailsvehicle", newItem);
        }

        // Display an HTML Form for vehicle "add new" use case
        // Without any data annotations

        public ActionResult CreateVehiclePlain()
        {
            return View(new VehicleAddPlainViewModel());
        }

        [HttpPost]
        public ActionResult CreateVehiclePlain(VehicleAddPlainViewModel newItem)
        {
            return View("detailsvehicleplain", newItem);
        }

        // ############################################################
        // Account methods

        // Renders a "details" view
        public ActionResult DetailsAccount(AccountAddViewModel item)
        {
            return View(item);
        }

        // Renders a "details" view
        public ActionResult DetailsAccountPlain(AccountAddPlainViewModel item)
        {
            return View(item);
        }

        // Display an HTML Form for account "add new" use case
        // With useful data annotations

        public ActionResult CreateAccount()
        {
            return View(new AccountAddViewModel());
        }

        [HttpPost]
        public ActionResult CreateAccount(AccountAddViewModel newItem)
        {
            return View("detailsaccount", newItem);
        }

        // Display an HTML Form for account "add new" use case
        // Without any data annotations

        public ActionResult CreateAccountPlain()
        {
            return View(new AccountAddPlainViewModel());
        }

        [HttpPost]
        public ActionResult CreateAccountPlain(AccountAddPlainViewModel newItem)
        {
            return View("detailsaccountplain", newItem);
        }
    }
}