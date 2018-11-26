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
    public class TipoResevaController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        // GET: TipoReseva
        public ActionResult Index()
        {
            return View(db.TipoReseva.ToList());
        }

        // GET: TipoReseva/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoReseva tipoReseva = db.TipoReseva.Find(id);
            if (tipoReseva == null)
            {
                return HttpNotFound();
            }
            return View(tipoReseva);
        }

        // GET: TipoReseva/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoReseva/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,descripcion")] TipoReseva tipoReseva)
        {
            if (ModelState.IsValid)
            {
                db.TipoReseva.Add(tipoReseva);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoReseva);
        }

        // GET: TipoReseva/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoReseva tipoReseva = db.TipoReseva.Find(id);
            if (tipoReseva == null)
            {
                return HttpNotFound();
            }
            return View(tipoReseva);
        }

        // POST: TipoReseva/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,descripcion")] TipoReseva tipoReseva)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoReseva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoReseva);
        }

        // GET: TipoReseva/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoReseva tipoReseva = db.TipoReseva.Find(id);
            if (tipoReseva == null)
            {
                return HttpNotFound();
            }
            return View(tipoReseva);
        }

        // POST: TipoReseva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoReseva tipoReseva = db.TipoReseva.Find(id);
            db.TipoReseva.Remove(tipoReseva);
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
