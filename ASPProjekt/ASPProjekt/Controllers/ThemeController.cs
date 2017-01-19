using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPProjekt.Controllers
{
    public class ThemeController : Controller
    {
        // GET: Theme
        public ActionResult ChangeTheme()
        {
            if ((bool)this.Session["__Theme"])
            {
                this.Session["__Theme"] = false;
            }
            else
            {
                this.Session["__Theme"] = true;
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}