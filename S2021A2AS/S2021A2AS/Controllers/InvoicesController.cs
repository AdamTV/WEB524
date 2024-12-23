using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A2AS.Controllers
{
    public class InvoicesController : Controller
    {
        Manager m = new Manager();

        // GET: Invoices
        public ActionResult Index()
        {
            return View(m.InvoiceGetAll());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int id)
        {
            return View(m.InvoiceGetByIdWithDetail(id));
        }
    }
}
