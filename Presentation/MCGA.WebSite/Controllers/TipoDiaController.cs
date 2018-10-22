using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MCGA.Data;

namespace MCGA.WebSite.Controllers
{
    public class TipoDiaController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        // GET: TipoDia
        public ActionResult Index()
        {
            return View(db.TipoDia.ToList());
        }

        // GET: TipoDia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDia tipoDia = db.TipoDia.Find(id);
            if (tipoDia == null)
            {
                return HttpNotFound();
            }
            return View(tipoDia);
        }

        // GET: TipoDia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,descripcion")] TipoDia tipoDia)
        {
            if (ModelState.IsValid)
            {
                db.TipoDia.Add(tipoDia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoDia);
        }

        // GET: TipoDia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDia tipoDia = db.TipoDia.Find(id);
            if (tipoDia == null)
            {
                return HttpNotFound();
            }
            return View(tipoDia);
        }

        // POST: TipoDia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,descripcion")] TipoDia tipoDia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoDia);
        }

        // GET: TipoDia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDia tipoDia = db.TipoDia.Find(id);
            if (tipoDia == null)
            {
                return HttpNotFound();
            }
            return View(tipoDia);
        }

        // POST: TipoDia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoDia tipoDia = db.TipoDia.Find(id);
            db.TipoDia.Remove(tipoDia);
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
