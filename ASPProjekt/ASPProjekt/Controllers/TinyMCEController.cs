using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPProjekt.Controllers
{
    using ASPProjekt.Models;

    public class TinyMCEController : Controller
    {
        // An action to display your TinyMCE editor
        public ActionResult Index()
        {
            return View();
        }

        // An action that will accept your Html Content
        [HttpPost]
        public ActionResult Index(ExampleClass model)
        {
            return View();
        }
    }
}