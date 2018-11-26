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
    public class AtencionController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        // GET: Atencion
        public ActionResult Index()
        {
            var atencion = db.Atencion.Include(a => a.Turno);
            return View(atencion.ToList());
        }

        // GET: Atencion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atencion atencion = db.Atencion.Find(id);
            if (atencion == null)
            {
                return HttpNotFound();
            }
            return View(atencion);
        }

        // GET: Atencion/Create
        public ActionResult Create()
        {
            ViewBag.turno_id = new SelectList(db.Turno, "Id", "Observaciones");
            return View();
        }

        // POST: Atencion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,turno_id,hora_llegada,hora_atencion,sintomas,diagnostico,bono_id,createdon,createdby,changedon,changedby")] Atencion atencion)
        {
            if (ModelState.IsValid)
            {
                db.Atencion.Add(atencion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.turno_id = new SelectList(db.Turno, "Id", "Observaciones", atencion.turno_id);
            return View(atencion);
        }

        // GET: Atencion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atencion atencion = db.Atencion.Find(id);
            if (atencion == null)
            {
                return HttpNotFound();
            }
            ViewBag.turno_id = new SelectList(db.Turno, "Id", "Observaciones", atencion.turno_id);
            return View(atencion);
        }

        // POST: Atencion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,turno_id,hora_llegada,hora_atencion,sintomas,diagnostico,bono_id,createdon,createdby,changedon,changedby")] Atencion atencion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atencion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.turno_id = new SelectList(db.Turno, "Id", "Observaciones", atencion.turno_id);
            return View(atencion);
        }

        // GET: Atencion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atencion atencion = db.Atencion.Find(id);
            if (atencion == null)
            {
                return HttpNotFound();
            }
            return View(atencion);
        }

        // POST: Atencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Atencion atencion = db.Atencion.Find(id);
            db.Atencion.Remove(atencion);
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
