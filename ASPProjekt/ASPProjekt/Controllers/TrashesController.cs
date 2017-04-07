namespace ASPProjekt.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Models;

    [Authorize]
    public class TrashesController : Controller
    {
        private readonly IRepository db;
        private IControllerIdentity identity;
        private bool reloadUserIdentity = false;

        public TrashesController()
        {
            this.db = new Repository(new ApplicationDbContext());
            this.reloadUserIdentity = true;
        }

        public TrashesController(IRepository context, IControllerIdentity identity)
        {
            this.db = context;
            this.identity = identity;
            this.reloadUserIdentity = false;
        }

        // GET: Trashes/Create
        public ActionResult Create()
        {
            if (this.reloadUserIdentity) this.identity = new ControllerIdentity(this.User.Identity);
            string id = this.identity.GetUserId();
            var user = this.db.Users.Include(x => x.Bins).FirstOrDefault(x => x.Id == id);
            if (user?.Bins != null && user.Bins.Count == 0)
            {
                return this.RedirectToAction("Create", "Bins");
            }

            this.ViewBag.BinId = new SelectList(this.db.Bins.Where(x => x.ApplicationUserId == id), "Id", "Name");
            return this.View("Create");
        }

        // POST: Trashes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,AddTime,BinId")] Trash trash)
        {
            if (this.ModelState.IsValid)
            {
                trash.AddTime = DateTime.Now;
                this.db.Add(trash);
                return this.RedirectToAction("Details", "Bins", new { id = trash.BinId });
            }
            if (this.reloadUserIdentity) this.identity = new ControllerIdentity(this.User.Identity);
            string id = this.identity.GetUserId();
            this.ViewBag.BinId = new SelectList(
                                     this.db.Bins.Where(x => x.ApplicationUserId == id),
                                     "Id",
                                     "Name",
                                     trash.BinId);
            return this.View("Create", trash);
        }

        // GET: Trashes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trash trash = this.db.Trash.FirstOrDefault(x => x.Id == id.Value);
            if (trash == null)
            {
                return this.HttpNotFound();
            }

            return this.View("Delete", trash);
        }

        // POST: Trashes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trash trash = this.db.Trash.FirstOrDefault(x => x.Id == id);
            this.db.Remove(trash);
            return this.RedirectToAction("Details", "Bins", new { id = trash.BinId });
        }

        // GET: Trashes/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trash trash =
                this.db.Trash.Include(x => x.Comments)
                    .Include(x => x.Bin)
                    .Include(x => x.Bin.ApplicationUser)
                    .FirstOrDefault(x => x.Id == id);
            if (trash == null)
            {
                return this.HttpNotFound();
            }

            return this.View("Details", trash);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Details(string commentContent, int trashId)
        {
            if (this.reloadUserIdentity) this.identity = new ControllerIdentity(this.User.Identity);
            var trash = this.db.Trash.Include(x => x.Comments).First(x => x.Id == trashId);
            if (string.IsNullOrEmpty(commentContent))
            {
                return this.View(trash);
            }

            var comment = new Comment
                              {
                                  ApplicationUserId = this.identity.GetUserId(),
                                  TrashId = trashId,
                                  Content = commentContent
                              };
            trash.Comments.Add(comment);
            this.db.Edit(trash);
            return this.View(trash);
        }

        // GET: Trashes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trash trash = this.db.Trash.FirstOrDefault(x => x.Id == id.Value);
            if (trash == null)
            {
                return this.HttpNotFound();
            }
            if (this.reloadUserIdentity) this.identity = new ControllerIdentity(this.User.Identity);
            string userId = this.identity.GetUserId();
            this.ViewBag.BinId = new SelectList(
                                     this.db.Bins.Where(x => x.ApplicationUserId == userId),
                                     "Id",
                                     "Name",
                                     trash.BinId);
            return this.View("Edit", trash);
        }

        // POST: Trashes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,AddTime,BinId")] Trash trash)
        {
            if (this.ModelState.IsValid)
            {
                trash.AddTime = DateTime.Now;
                this.db.Edit(trash);
                return this.RedirectToAction("Details", "Bins", new { id = trash.BinId });
            }
            if (this.reloadUserIdentity) this.identity = new ControllerIdentity(this.User.Identity);
            string userId = this.identity.GetUserId();
            this.ViewBag.BinId = new SelectList(
                                     this.db.Bins.Where(x => x.ApplicationUserId == userId),
                                     "Id",
                                     "Name",
                                     trash.BinId);
            return this.View("Edit", trash);
        }

        // GET: Trashes
        [AllowAnonymous]
        public ActionResult Index()
        {
            var trash = this.db.Trash.Include(t => t.Bin);
            return this.View("Index", trash.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}