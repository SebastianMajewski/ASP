using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPProjekt.Controllers
{
    using System.Data.Entity;
    using System.Threading;

    using ASPProjekt.Models;

    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.Trash =
                this.db.Trash.Include(x => x.Comments).OrderByDescending(x => x.Comments.Count).Take(10).ToList();
            model.Bins = this.db.Bins.Include(x => x.Trash).OrderByDescending(x => x.Trash.Count).Take(10).ToList();
            var users = this.db.Users.Include(x => x.Bins).ToList();
            var table =
                (from user in users
                 let count = user.Bins.Sum(bin => bin.Trash.Count)
                 select new Tuple<ApplicationUser, int>(user, count)).ToList();
            model.Users = table.OrderByDescending(x => x.Item2).Take(10).Select(x => x.Item1).ToList();
            return View(model);
        }
    }
}