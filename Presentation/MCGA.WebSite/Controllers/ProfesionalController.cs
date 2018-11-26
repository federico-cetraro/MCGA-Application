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
    public class ProfesionalController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        // GET: Profesional
        public ActionResult Index()
        {
            var profesional = db.Profesional.Include(p => p.TipoDocumento);
            return View(profesional.ToList());
        }

        // GET: Profesional/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesional profesional = db.Profesional.Find(id);
            if (profesional == null)
            {
                return HttpNotFound();
            }
            return View(profesional);
        }

        // GET: Profesional/Create
        public ActionResult Create()
        {
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento, "Id", "descripcion");
            return View();
        }

        // POST: Profesional/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Apellido,TipoDocumentoId,Numero,Direccion,Telefono,Email,FechaNacimiento,Matricula,Foto,createdon,createdby,changedon,changedby,deletedon,deletedby,isdeleted")] Profesional profesional)
        {
            if (ModelState.IsValid)
            {
                db.Profesional.Add(profesional);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento, "Id", "descripcion", profesional.TipoDocumentoId);
            return View(profesional);
        }

        // GET: Profesional/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesional profesional = db.Profesional.Find(id);
            if (profesional == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento, "Id", "descripcion", profesional.TipoDocumentoId);
            return View(profesional);
        }

        // POST: Profesional/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Apellido,TipoDocumentoId,Numero,Direccion,Telefono,Email,FechaNacimiento,Matricula,Foto,createdon,createdby,changedon,changedby,deletedon,deletedby,isdeleted")] Profesional profesional)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profesional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento, "Id", "descripcion", profesional.TipoDocumentoId);
            return View(profesional);
        }

        // GET: Profesional/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesional profesional = db.Profesional.Find(id);
            if (profesional == null)
            {
                return HttpNotFound();
            }
            return View(profesional);
        }

        // POST: Profesional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profesional profesional = db.Profesional.Find(id);
            db.Profesional.Remove(profesional);
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
