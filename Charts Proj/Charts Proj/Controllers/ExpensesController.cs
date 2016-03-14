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
    public class ExpensesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Expenses
        public ActionResult Index()
        {
            ViewBag.dates = db.Expenses.Select(x => x.Date).ToList();
            ViewBag.intArray = db.Expenses.Select(x => x.Total).ToArray();
            var expenses = db.Expenses.Include(e => e.ExpenseCategory);
            return View(expenses.ToList());
        }

        public ActionResult PartialQuarterly()
        {
            ViewBag.Q1 = db.Expenses.Select(x => x.Total).Sum(x => x);
            ViewBag.Q2 = 0;
            ViewBag.Q3 = 0;
            ViewBag.Q4 = 0;
            return PartialView("Quarterly");
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        
        public ActionResult CreatePartial()
        {
            ViewBag.ExpenseCategoryID = new SelectList(db.ExpenseCategories, "ID", "Name");
            //if (ModelState.IsValid)
            //{
            //    Expense expense = new Expense();
            //    expense.RegisteredUserID = User.Identity.GetUserId();
            //    ViewBag.ExpenseCategoryID = new SelectList(db.ExpenseCategories, "ID", "Name", expense.ExpenseCategoryID);
            //    db.Expenses.Add(expense);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //    //return View("Index");
            //}
            return PartialView("Create");
        }

        public ActionResult Create()
        {
            ViewBag.ExpenseCategoryID = new SelectList(db.ExpenseCategories, "ID", "Name");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Payee,Total,Date,ExpenseCategoryID")] Expense expense)
        {
            ViewBag.ExpenseCategoryID = new SelectList(db.ExpenseCategories, "ID", "Name", expense.ExpenseCategoryID);
            if (ModelState.IsValid)
            {
                expense.RegisteredUserID = User.Identity.GetUserId();
                ViewBag.ExpenseCategoryID = new SelectList(db.ExpenseCategories, "ID", "Name", expense.ExpenseCategoryID);
                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
                //return View("Index");
            }

            return View("Index");
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExpenseCategoryID = new SelectList(db.ExpenseCategories, "ID", "Name", expense.ExpenseCategoryID);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Payee,Total,Date,ExpenseCategoryID")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExpenseCategoryID = new SelectList(db.ExpenseCategories, "ID", "Name", expense.ExpenseCategoryID);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
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
