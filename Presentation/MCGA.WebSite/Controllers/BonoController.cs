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
    public class BonoController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        // GET: Bono
        public ActionResult Index()
        {
            var bono = db.Bono.Include(b => b.Afiliado).Include(b => b.Plan);
            return View(bono.ToList());
        }

        // GET: Bono/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bono bono = db.Bono.Find(id);
            if (bono == null)
            {
                return HttpNotFound();
            }
            return View(bono);
        }

        // GET: Bono/Create
        public ActionResult Create()
        {
            ViewBag.afiliado_id = new SelectList(db.Afiliado, "Id", "Nombre");
            ViewBag.plan_id = new SelectList(db.Plan, "Id", "descripcion");
            return View();
        }

        // POST: Bono/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,plan_id,afiliado_id,fecha_compra,fecha_impresion,consumido")] Bono bono)
        {
            if (ModelState.IsValid)
            {
                db.Bono.Add(bono);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.afiliado_id = new SelectList(db.Afiliado, "Id", "Nombre", bono.afiliado_id);
            ViewBag.plan_id = new SelectList(db.Plan, "Id", "descripcion", bono.plan_id);
            return View(bono);
        }

        // GET: Bono/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bono bono = db.Bono.Find(id);
            if (bono == null)
            {
                return HttpNotFound();
            }
            ViewBag.afiliado_id = new SelectList(db.Afiliado, "Id", "Nombre", bono.afiliado_id);
            ViewBag.plan_id = new SelectList(db.Plan, "Id", "descripcion", bono.plan_id);
            return View(bono);
        }

        // POST: Bono/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,plan_id,afiliado_id,fecha_compra,fecha_impresion,consumido")] Bono bono)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bono).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.afiliado_id = new SelectList(db.Afiliado, "Id", "Nombre", bono.afiliado_id);
            ViewBag.plan_id = new SelectList(db.Plan, "Id", "descripcion", bono.plan_id);
            return View(bono);
        }

        // GET: Bono/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bono bono = db.Bono.Find(id);
            if (bono == null)
            {
                return HttpNotFound();
            }
            return View(bono);
        }

        // POST: Bono/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bono bono = db.Bono.Find(id);
            db.Bono.Remove(bono);
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
