using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduardoPrimavera.Controllers
{
    public class EditController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Edit";

            return View();
        }
    }
}
