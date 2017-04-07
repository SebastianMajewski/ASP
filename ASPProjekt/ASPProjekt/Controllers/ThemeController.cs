namespace ASPProjekt.Controllers
{
    using System.Web.Mvc;

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