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
    public class EspecialidadesProfesionalController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        public JsonResult GetEspecialidadProfesionalByProfesional(string Areas, string term = "", int idProfesional = 0)
        {
            var lista = db.EspecialidadesProfesional.Include(e => e.Especialidad).Include(e => e.Profesional).ToList().Where(o => o.ProfesionalId == idProfesional && o.Especialidad.descripcion.ToUpper().Contains(term.ToUpper())).OrderBy(o => o.Especialidad.descripcion).Select(o => new { Id = o.Id, Name = o.Especialidad.descripcion }).ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEspecialidadProfesionalByEspecialidad(string Areas, string term = "", int idEspecialidad = 0)
        {
            var lista = db.EspecialidadesProfesional.Include(e => e.Especialidad).Include(e => e.Profesional).ToList().Where(o => o.EspecialidadId == idEspecialidad && (o.Profesional.Nombre.ToUpper().Contains(term.ToUpper()) || o.Profesional.Apellido.ToUpper().Contains(term.ToUpper()))).OrderBy(o => o.Profesional.Nombre).OrderBy(o => o.Profesional.Apellido).Select(o => new { Id = o.Id, Name = string.Format("{0} {1}", o.Profesional.Nombre, o.Profesional.Apellido) }).ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        // GET: EspecialidadesProfesional
        public ActionResult Index()
        {
            var especialidadesProfesional = db.EspecialidadesProfesional.Include(e => e.Especialidad).Include(e => e.Profesional);
            return View(especialidadesProfesional.ToList());
        }

        // GET: EspecialidadesProfesional/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecialidadesProfesional especialidadesProfesional = db.EspecialidadesProfesional.Find(id);
            if (especialidadesProfesional == null)
            {
                return HttpNotFound();
            }
            return View(especialidadesProfesional);
        }

        // GET: EspecialidadesProfesional/Create
        public ActionResult Create()
        {
            ViewBag.EspecialidadId = new SelectList(db.Especialidad, "Id", "descripcion");
            ViewBag.ProfesionalId = new SelectList(db.Profesional, "Id", "Nombre");
            return View();
        }

        // POST: EspecialidadesProfesional/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EspecialidadId,ProfesionalId,createdon,createdby,changedon,changedby")] EspecialidadesProfesional especialidadesProfesional)
        {
            if (ModelState.IsValid)
            {
                db.EspecialidadesProfesional.Add(especialidadesProfesional);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EspecialidadId = new SelectList(db.Especialidad, "Id", "descripcion", especialidadesProfesional.EspecialidadId);
            ViewBag.ProfesionalId = new SelectList(db.Profesional, "Id", "Nombre", especialidadesProfesional.ProfesionalId);
            return View(especialidadesProfesional);
        }

        // GET: EspecialidadesProfesional/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecialidadesProfesional especialidadesProfesional = db.EspecialidadesProfesional.Find(id);
            if (especialidadesProfesional == null)
            {
                return HttpNotFound();
            }
            ViewBag.EspecialidadId = new SelectList(db.Especialidad, "Id", "descripcion", especialidadesProfesional.EspecialidadId);
            ViewBag.ProfesionalId = new SelectList(db.Profesional, "Id", "Nombre", especialidadesProfesional.ProfesionalId);
            return View(especialidadesProfesional);
        }

        // POST: EspecialidadesProfesional/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EspecialidadId,ProfesionalId,createdon,createdby,changedon,changedby")] EspecialidadesProfesional especialidadesProfesional)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especialidadesProfesional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EspecialidadId = new SelectList(db.Especialidad, "Id", "descripcion", especialidadesProfesional.EspecialidadId);
            ViewBag.ProfesionalId = new SelectList(db.Profesional, "Id", "Nombre", especialidadesProfesional.ProfesionalId);
            return View(especialidadesProfesional);
        }

        // GET: EspecialidadesProfesional/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecialidadesProfesional especialidadesProfesional = db.EspecialidadesProfesional.Find(id);
            if (especialidadesProfesional == null)
            {
                return HttpNotFound();
            }
            return View(especialidadesProfesional);
        }

        // POST: EspecialidadesProfesional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EspecialidadesProfesional especialidadesProfesional = db.EspecialidadesProfesional.Find(id);
            db.EspecialidadesProfesional.Remove(especialidadesProfesional);
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
