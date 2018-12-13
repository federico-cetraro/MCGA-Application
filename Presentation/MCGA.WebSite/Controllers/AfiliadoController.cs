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
    public class AfiliadoController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        public JsonResult GetAfiliado(string Areas, string term = "")
        {
            var lista = db.Afiliado.Include(a => a.EstadoCivil).Include(a => a.Plan).Include(a => a.TipoDocumento).Include(a => a.TipoSexo).Where(a => a.isdeleted == false).ToList().Where(o => o.Nombre.ToUpper().Contains(term.ToUpper()) || o.Apellido.ToUpper().Contains(term.ToUpper())).OrderBy(o => o.Nombre).OrderBy(o => o.Apellido).Select(o => new { Id = o.Id, Name = string.Format("{0} {1} Nº {2} ({3} {4})", o.Nombre, o.Apellido, o.NumeroAfiliado, o.TipoDocumento.descripcion, o.Numero) }).ToList();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        // GET: Afiliado
        public ActionResult Index()
        {
            var afiliado = db.Afiliado.Include(a => a.EstadoCivil).Include(a => a.TipoDocumento).Include(a => a.TipoSexo);
            return View(afiliado.ToList());
        }

        // GET: Afiliado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afiliado afiliado = db.Afiliado.Find(id);
            if (afiliado == null)
            {
                return HttpNotFound();
            }
            return View(afiliado);
        }

        // GET: Afiliado/Create
        public ActionResult Create()
        {
            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivil, "Id", "descripcion");
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento, "Id", "descripcion");
            ViewBag.TipoSexoId = new SelectList(db.TipoSexo, "Id", "descripcion");
            ViewBag.PlanId = new SelectList(db.Plan.Where(p => p.isdeleted == false).ToList(), "Id", "descripcion");
            return View();
        }

        // POST: Afiliado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Apellido,TipoDocumentoId,Numero,Direccion,Telefono,Email,TipoSexoId,FechaNacimiento,EstadoCivilId,NumeroAfiliado,PlanId,Foto,Habilitado,createdon,createdby,changedon,changedby,deletedon,deletedby,isdeleted")] Afiliado afiliado)
        {
            if (ModelState.IsValid)
            {
                db.Afiliado.Add(afiliado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivil, "Id", "descripcion", afiliado.EstadoCivilId);
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento, "Id", "descripcion", afiliado.TipoDocumentoId);
            ViewBag.TipoSexoId = new SelectList(db.TipoSexo, "Id", "descripcion", afiliado.TipoSexoId);
            ViewBag.PlanId = new SelectList(db.Plan.Where(p => p.isdeleted == false).ToList(), "Id", "descripcion");
            return View(afiliado);
        }

        // GET: Afiliado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afiliado afiliado = db.Afiliado.Find(id);
            if (afiliado == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivil, "Id", "descripcion", afiliado.EstadoCivilId);
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento, "Id", "descripcion", afiliado.TipoDocumentoId);
            ViewBag.TipoSexoId = new SelectList(db.TipoSexo, "Id", "descripcion", afiliado.TipoSexoId);
            ViewBag.PlanId = new SelectList(db.Plan.Where(p => p.isdeleted == false).ToList(), "Id", "descripcion");
            return View(afiliado);
        }

        // POST: Afiliado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Apellido,TipoDocumentoId,Numero,Direccion,Telefono,Email,TipoSexoId,FechaNacimiento,EstadoCivilId,NumeroAfiliado,PlanId,Foto,Habilitado,createdon,createdby,changedon,changedby,deletedon,deletedby,isdeleted")] Afiliado afiliado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(afiliado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivil, "Id", "descripcion", afiliado.EstadoCivilId);
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento, "Id", "descripcion", afiliado.TipoDocumentoId);
            ViewBag.TipoSexoId = new SelectList(db.TipoSexo, "Id", "descripcion", afiliado.TipoSexoId);
            ViewBag.PlanId = new SelectList(db.Plan.Where(p => p.isdeleted == false).ToList(), "Id", "descripcion");
            return View(afiliado);
        }

        // GET: Afiliado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afiliado afiliado = db.Afiliado.Find(id);
            if (afiliado == null)
            {
                return HttpNotFound();
            }
            return View(afiliado);
        }

        // POST: Afiliado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Afiliado afiliado = db.Afiliado.Find(id);
            db.Afiliado.Remove(afiliado);
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
