using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPProjekt.Models;

namespace ASPProjekt.Controllers
{
    using Microsoft.AspNet.Identity;

    [Authorize]
    public class BinsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bins
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Bins.ToList());
        }

        // GET: Bins/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bin bin = db.Bins.Find(id);
            if (bin == null)
            {
                return HttpNotFound();
            }
            return View(bin);
        }

        // GET: Bins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Bin bin)
        {
            if (ModelState.IsValid)
            {
                bin.ApplicationUserId = this.User.Identity.GetUserId();
                db.Bins.Add(bin);
                this.db.SaveChanges();
                return RedirectToAction("Details", "Account", new { userName = this.User.Identity.GetUserName() });
            }

            return View(bin);
        }

        // GET: Bins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bin bin = db.Bins.Find(id);
            if (bin == null)
            {
                return HttpNotFound();
            }
            return View(bin);
        }

        // POST: Bins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Bin bin)
        {
            if (ModelState.IsValid)
            {
                bin.ApplicationUserId = this.User.Identity.GetUserId();
                db.Entry(bin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Account", new { userName = this.User.Identity.GetUserName() });
            }
            return View(bin);
        }

        // GET: Bins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bin bin = db.Bins.Find(id);
            if (bin == null)
            {
                return HttpNotFound();
            }
            return View(bin);
        }

        // POST: Bins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bin bin = db.Bins.Find(id);
            db.Bins.Remove(bin);
            db.SaveChanges();
            return RedirectToAction("Details", "Account", new { userName = this.User.Identity.GetUserName() });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
