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
{   [Authorize]
    public class HistorialAfiliadoController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        // GET: HistorialAfiliado
        public ActionResult Index()
        {
            var historialAfiliado = db.HistorialAfiliado.Include(h => h.Afiliado).Include(h => h.Plan);
            return View(historialAfiliado.ToList());
        }

        // GET: HistorialAfiliado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialAfiliado historialAfiliado = db.HistorialAfiliado.Find(id);
            if (historialAfiliado == null)
            {
                return HttpNotFound();
            }
            return View(historialAfiliado);
        }

        // GET: HistorialAfiliado/Create
        public ActionResult Create()
        {
            ViewBag.afiliado_id = new SelectList(db.Afiliado, "Id", "Nombre");
            ViewBag.plan_activo = new SelectList(db.Plan, "Id", "descripcion");
            return View();
        }

        // POST: HistorialAfiliado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,afiliado_id,fecha,plan_activo,motivo_cambio,createdon,createdby,changedon,changedby")] HistorialAfiliado historialAfiliado)
        {
            if (ModelState.IsValid)
            {
                db.HistorialAfiliado.Add(historialAfiliado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.afiliado_id = new SelectList(db.Afiliado, "Id", "Nombre", historialAfiliado.afiliado_id);
            ViewBag.plan_activo = new SelectList(db.Plan, "Id", "descripcion", historialAfiliado.plan_activo);
            return View(historialAfiliado);
        }

        // GET: HistorialAfiliado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialAfiliado historialAfiliado = db.HistorialAfiliado.Find(id);
            if (historialAfiliado == null)
            {
                return HttpNotFound();
            }
            ViewBag.afiliado_id = new SelectList(db.Afiliado, "Id", "Nombre", historialAfiliado.afiliado_id);
            ViewBag.plan_activo = new SelectList(db.Plan, "Id", "descripcion", historialAfiliado.plan_activo);
            return View(historialAfiliado);
        }

        // POST: HistorialAfiliado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,afiliado_id,fecha,plan_activo,motivo_cambio,createdon,createdby,changedon,changedby")] HistorialAfiliado historialAfiliado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historialAfiliado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.afiliado_id = new SelectList(db.Afiliado, "Id", "Nombre", historialAfiliado.afiliado_id);
            ViewBag.plan_activo = new SelectList(db.Plan, "Id", "descripcion", historialAfiliado.plan_activo);
            return View(historialAfiliado);
        }

        // GET: HistorialAfiliado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialAfiliado historialAfiliado = db.HistorialAfiliado.Find(id);
            if (historialAfiliado == null)
            {
                return HttpNotFound();
            }
            return View(historialAfiliado);
        }

        // POST: HistorialAfiliado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistorialAfiliado historialAfiliado = db.HistorialAfiliado.Find(id);
            db.HistorialAfiliado.Remove(historialAfiliado);
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
