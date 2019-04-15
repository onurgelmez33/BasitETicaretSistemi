using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ETicaret.Data;

namespace ETicaret.Web.Areas.Admin.Controllers
{
    public class KargolamaSureController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Admin/KargolamaSure
        public ActionResult Index()
        {
            return View(db.KargolamaSure.ToList());
        }

        // GET: Admin/KargolamaSure/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KargolamaSure kargolamaSure = db.KargolamaSure.Find(id);
            if (kargolamaSure == null)
            {
                return HttpNotFound();
            }
            return View(kargolamaSure);
        }

        // GET: Admin/KargolamaSure/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KargolamaSure/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,KargolamaSuresi,RenkKodu")] KargolamaSure kargolamaSure)
        {
            if (ModelState.IsValid)
            {
                db.KargolamaSure.Add(kargolamaSure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kargolamaSure);
        }

        // GET: Admin/KargolamaSure/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KargolamaSure kargolamaSure = db.KargolamaSure.Find(id);
            if (kargolamaSure == null)
            {
                return HttpNotFound();
            }
            return View(kargolamaSure);
        }

        // POST: Admin/KargolamaSure/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,KargolamaSuresi,RenkKodu")] KargolamaSure kargolamaSure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kargolamaSure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kargolamaSure);
        }

        // GET: Admin/KargolamaSure/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KargolamaSure kargolamaSure = db.KargolamaSure.Find(id);
            if (kargolamaSure == null)
            {
                return HttpNotFound();
            }
            return View(kargolamaSure);
        }

        // POST: Admin/KargolamaSure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KargolamaSure kargolamaSure = db.KargolamaSure.Find(id);
            db.KargolamaSure.Remove(kargolamaSure);
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
