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
    [Authorize]
    public class TipoCancelacionController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        // GET: TipoCancelacion
        public ActionResult Index()
        {
            return View(db.TipoCancelacion.ToList());
        }

        // GET: TipoCancelacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCancelacion tipoCancelacion = db.TipoCancelacion.Find(id);
            if (tipoCancelacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoCancelacion);
        }

        // GET: TipoCancelacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoCancelacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,descripcion")] TipoCancelacion tipoCancelacion)
        {
            if (ModelState.IsValid)
            {
                db.TipoCancelacion.Add(tipoCancelacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoCancelacion);
        }

        // GET: TipoCancelacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCancelacion tipoCancelacion = db.TipoCancelacion.Find(id);
            if (tipoCancelacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoCancelacion);
        }

        // POST: TipoCancelacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,descripcion")] TipoCancelacion tipoCancelacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoCancelacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoCancelacion);
        }

        // GET: TipoCancelacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCancelacion tipoCancelacion = db.TipoCancelacion.Find(id);
            if (tipoCancelacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoCancelacion);
        }

        // POST: TipoCancelacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoCancelacion tipoCancelacion = db.TipoCancelacion.Find(id);
            db.TipoCancelacion.Remove(tipoCancelacion);
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
