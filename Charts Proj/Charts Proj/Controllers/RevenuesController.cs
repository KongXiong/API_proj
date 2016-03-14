using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Charts_Proj.Models;
using Microsoft.AspNet.Identity;

namespace Charts_Proj.Controllers
{
    public class RevenuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Revenues
        public ActionResult Index()
        {
            ViewBag.dates = db.Revenues.Select(x => x.Date).ToList();
            ViewBag.totals = db.Revenues.Select(x => x.Total).ToArray();
            var revenues = db.Revenues.Include(r => r.RevenueCategory);
            return View(revenues.ToList());
        }

        public ActionResult PartialQuarterly()
        {
            ViewBag.Q1 = db.Revenues.Select(x => x.Total).Sum(x => x);
            ViewBag.Q2 = 0;
            ViewBag.Q3 = 0;
            ViewBag.Q4 = 0;
            return PartialView("Quarterly");
        }

        public ActionResult Quarterly()
        {
           
            return View();
        }

        // GET: Revenues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revenue revenue = db.Revenues.Find(id);
            if (revenue == null)
            {
                return HttpNotFound();
            }
            return View(revenue);
        }

        // GET: Revenues/Create
        public ActionResult CreatePartial()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "Firstname");
            ViewBag.RevenueCategoryID = new SelectList(db.RevenueCategories, "ID", "Name");
            return PartialView("Create");
        }

        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "Firstname");
            ViewBag.RevenueCategoryID = new SelectList(db.RevenueCategories, "ID", "Name");
            return View();
        }

        // POST: Revenues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, Total, Date, RegisteredUserID, ClientID, RevenueCategoryID")] Revenue revenue)
        {
            if (ModelState.IsValid)
            {
                revenue.RegisteredUserID = User.Identity.GetUserId();
                //ViewBag.RevenueCategoryID = new SelectList(db.RevenueCategories, "ID", "Name", revenue.RevenueCategoryID);
                db.Revenues.Add(revenue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RevenueCategoryID = new SelectList(db.RevenueCategories, "ID", "Name", revenue.RevenueCategoryID);
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "Firstname", revenue.ClientID);
            return View(revenue);
        }

        // GET: Revenues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revenue revenue = db.Revenues.Find(id);
            if (revenue == null)
            {
                return HttpNotFound();
            }
            ViewBag.RevenueCategoryID = new SelectList(db.RevenueCategories, "ID", "Name", revenue.RevenueCategoryID);
            return View(revenue);
        }

        // POST: Revenues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,RegisteredUserID,ClientID,RevenueCategoryID")] Revenue revenue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(revenue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RevenueCategoryID = new SelectList(db.RevenueCategories, "ID", "Name", revenue.RevenueCategoryID);
            return View(revenue);
        }

        // GET: Revenues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revenue revenue = db.Revenues.Find(id);
            if (revenue == null)
            {
                return HttpNotFound();
            }
            return View(revenue);
        }

        // POST: Revenues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Revenue revenue = db.Revenues.Find(id);
            db.Revenues.Remove(revenue);
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
