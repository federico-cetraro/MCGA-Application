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
    public class EspecialidadController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        public JsonResult GetEspecialidad(string Areas, string term = "")
        {
            var lista = db.Especialidad.Include(e => e.TipoEspecialidad).ToList().Where(o => o.descripcion.ToUpper().Contains(term.ToUpper())).OrderBy(o => o.descripcion).Select(o => new { Id = o.Id, Name = o.descripcion }).ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        // GET: Especialidad
        public ActionResult Index()
        {
            var especialidad = db.Especialidad.Include(e => e.TipoEspecialidad);
            return View(especialidad.ToList());
        }

        // GET: Especialidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidad especialidad = db.Especialidad.Find(id);
            if (especialidad == null)
            {
                return HttpNotFound();
            }
            return View(especialidad);
        }

        // GET: Especialidad/Create
        public ActionResult Create()
        {
            ViewBag.TipoEspecialidadId = new SelectList(db.TipoEspecialidad, "Id", "descripcion");
            return View();
        }

        // POST: Especialidad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,descripcion,frecuencia,TipoEspecialidadId")] Especialidad especialidad)
        {
            if (ModelState.IsValid)
            {
                db.Especialidad.Add(especialidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoEspecialidadId = new SelectList(db.TipoEspecialidad, "Id", "descripcion", especialidad.TipoEspecialidadId);
            return View(especialidad);
        }

        // GET: Especialidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidad especialidad = db.Especialidad.Find(id);
            if (especialidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoEspecialidadId = new SelectList(db.TipoEspecialidad, "Id", "descripcion", especialidad.TipoEspecialidadId);
            return View(especialidad);
        }

        // POST: Especialidad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,descripcion,frecuencia,TipoEspecialidadId")] Especialidad especialidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especialidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoEspecialidadId = new SelectList(db.TipoEspecialidad, "Id", "descripcion", especialidad.TipoEspecialidadId);
            return View(especialidad);
        }

        // GET: Especialidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidad especialidad = db.Especialidad.Find(id);
            if (especialidad == null)
            {
                return HttpNotFound();
            }
            return View(especialidad);
        }

        // POST: Especialidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Especialidad especialidad = db.Especialidad.Find(id);
            db.Especialidad.Remove(especialidad);
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
