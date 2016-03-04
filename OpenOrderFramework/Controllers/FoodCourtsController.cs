using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OpenOrderFramework.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace OpenOrderFramework.Controllers
{
    public class FoodCourtsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FoodCourts
        public ActionResult Index()
        {
            return View(db.FoodCourts.ToList());
        }

        // GET: FoodCourts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodCourt foodCourt = db.FoodCourts.Find(id);
            if (foodCourt == null)
            {
                return HttpNotFound();
            }
            return View(foodCourt);
        }

        // GET: FoodCourts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FoodCourts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodCourt foodCourt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.FoodCourts.Add(foodCourt);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }

            return View(foodCourt);
        }

        // GET: FoodCourts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodCourt foodCourt = db.FoodCourts.Find(id);
            if (foodCourt == null)
            {
                return HttpNotFound();
            }
            return View(foodCourt);
        }

        // POST: FoodCourts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,InternalImage,FoodCourtPictureUrl")] FoodCourt foodCourt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodCourt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodCourt);
        }

        // GET: FoodCourts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodCourt foodCourt = db.FoodCourts.Find(id);
            if (foodCourt == null)
            {
                return HttpNotFound();
            }
            return View(foodCourt);
        }

        // POST: FoodCourts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodCourt foodCourt = db.FoodCourts.Find(id);
            db.FoodCourts.Remove(foodCourt);
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
