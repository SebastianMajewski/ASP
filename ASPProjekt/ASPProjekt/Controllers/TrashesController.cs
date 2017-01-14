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

    public class TrashesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trashes
        public ActionResult Index()
        {
            var trash = db.Trash.Include(t => t.Bin);
            return View(trash.ToList());
        }

        // GET: Trashes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trash trash = db.Trash.Include(x => x.Comments).FirstOrDefault(x => x.Id == id);
            if (trash == null)
            {
                return HttpNotFound();
            }
            return View(trash);
        }

        [HttpPost]
        public ActionResult Details(string commentContent, int trashId)
        {
            var trash = this.db.Trash.Include(x => x.Comments).First(x => x.Id == trashId);
            var comment = new Comment
            {
                ApplicationUserId = this.User.Identity.GetUserId(),
                TrashId = trashId,
                Content = commentContent
            };
            trash.Comments.Add(comment);
            this.db.SaveChanges();
            return this.View(trash);
        }

        // GET: Trashes/Create
        public ActionResult Create()
        {
            ViewBag.BinId = new SelectList(db.Bins, "Id", "Name");
            return View();
        }

        // POST: Trashes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,AddTime,BinId")] Trash trash)
        {
            if (ModelState.IsValid)
            {
                trash.AddTime = DateTime.Now;
                db.Trash.Add(trash);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BinId = new SelectList(db.Bins, "Id", "Name", trash.BinId);
            return View(trash);
        }

        // GET: Trashes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trash trash = db.Trash.Find(id);
            if (trash == null)
            {
                return HttpNotFound();
            }
            ViewBag.BinId = new SelectList(db.Bins, "Id", "Name", trash.BinId);
            return View(trash);
        }

        // POST: Trashes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,AddTime,BinId")] Trash trash)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trash).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BinId = new SelectList(db.Bins, "Id", "Name", trash.BinId);
            return View(trash);
        }

        // GET: Trashes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trash trash = db.Trash.Find(id);
            if (trash == null)
            {
                return HttpNotFound();
            }
            return View(trash);
        }

        // POST: Trashes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trash trash = db.Trash.Find(id);
            db.Trash.Remove(trash);
            db.SaveChanges();
            return RedirectToAction("Index");
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
