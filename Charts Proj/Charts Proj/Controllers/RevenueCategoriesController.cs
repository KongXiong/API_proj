using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Charts_Proj.Models;

namespace Charts_Proj.Controllers
{
    public class RevenueCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RevenueCategories
        public ActionResult Index()
        {
            return View(db.RevenueCategories.ToList());
        }

        // GET: RevenueCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevenueCategory revenueCategory = db.RevenueCategories.Find(id);
            if (revenueCategory == null)
            {
                return HttpNotFound();
            }
            return View(revenueCategory);
        }

        public ActionResult CreatePartial()
        {
            //ViewBag.RevenueCategoryID = new SelectList(db.RevenueCategories, "ID", "Name");
            return PartialView("Create");
        }

        // GET: RevenueCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RevenueCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] RevenueCategory revenueCategory)
        {
            if (ModelState.IsValid)
            {
                db.RevenueCategories.Add(revenueCategory);
                db.SaveChanges();
                return RedirectToAction("Index", "Revenues");
            }

            return View(revenueCategory);
        }

        // GET: RevenueCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevenueCategory revenueCategory = db.RevenueCategories.Find(id);
            if (revenueCategory == null)
            {
                return HttpNotFound();
            }
            return View(revenueCategory);
        }

        // POST: RevenueCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] RevenueCategory revenueCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(revenueCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(revenueCategory);
        }

        // GET: RevenueCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevenueCategory revenueCategory = db.RevenueCategories.Find(id);
            if (revenueCategory == null)
            {
                return HttpNotFound();
            }
            return View(revenueCategory);
        }

        // POST: RevenueCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RevenueCategory revenueCategory = db.RevenueCategories.Find(id);
            db.RevenueCategories.Remove(revenueCategory);
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
