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
    public class CancelacionController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        // GET: Cancelacion
        public ActionResult Index()
        {
            var cancelacion = db.Cancelacion.Include(c => c.TipoCancelacion).Include(c => c.Turno);
            return View(cancelacion.ToList());
        }

        // GET: Cancelacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancelacion cancelacion = db.Cancelacion.Find(id);
            if (cancelacion == null)
            {
                return HttpNotFound();
            }
            return View(cancelacion);
        }

        // GET: Cancelacion/Create
        public ActionResult Create()
        {
            ViewBag.tipo_cancel_id = new SelectList(db.TipoCancelacion, "Id", "descripcion");
            ViewBag.turno_id = new SelectList(db.Turno, "Id", "Observaciones");
            return View();
        }

        // POST: Cancelacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,turno_id,detalle_cancel,fecha_cancel,tipo_cancel_id,createdon,createdby,changedon,changedby")] Cancelacion cancelacion)
        {
            if (ModelState.IsValid)
            {
                db.Cancelacion.Add(cancelacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tipo_cancel_id = new SelectList(db.TipoCancelacion, "Id", "descripcion", cancelacion.tipo_cancel_id);
            ViewBag.turno_id = new SelectList(db.Turno, "Id", "Observaciones", cancelacion.turno_id);
            return View(cancelacion);
        }

        // GET: Cancelacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancelacion cancelacion = db.Cancelacion.Find(id);
            if (cancelacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.tipo_cancel_id = new SelectList(db.TipoCancelacion, "Id", "descripcion", cancelacion.tipo_cancel_id);
            ViewBag.turno_id = new SelectList(db.Turno, "Id", "Observaciones", cancelacion.turno_id);
            return View(cancelacion);
        }

        // POST: Cancelacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,turno_id,detalle_cancel,fecha_cancel,tipo_cancel_id,createdon,createdby,changedon,changedby")] Cancelacion cancelacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cancelacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tipo_cancel_id = new SelectList(db.TipoCancelacion, "Id", "descripcion", cancelacion.tipo_cancel_id);
            ViewBag.turno_id = new SelectList(db.Turno, "Id", "Observaciones", cancelacion.turno_id);
            return View(cancelacion);
        }

        // GET: Cancelacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancelacion cancelacion = db.Cancelacion.Find(id);
            if (cancelacion == null)
            {
                return HttpNotFound();
            }
            return View(cancelacion);
        }

        // POST: Cancelacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cancelacion cancelacion = db.Cancelacion.Find(id);
            db.Cancelacion.Remove(cancelacion);
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
