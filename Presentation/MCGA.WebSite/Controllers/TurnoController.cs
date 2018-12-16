using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MCGA.Data;
using Rotativa;

namespace MCGA.WebSite.Controllers
{   [Authorize]
    [ValidateInput(false)]
    public class TurnoController : Controller
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private MedicureContexto db = new MedicureContexto();

        // GET: Turno
        [Authorize]
        public ActionResult Index()
        {
            var turno = db.Turno.Include(t => t.Afiliado).Include(t => t.EspecialidadesProfesional);
            logger.Debug("Se accedio al Index de Turnos");
            return View(turno.ToList());
        }
        [Authorize]
        [HttpGet]
        [Route("Turno/_ListadoPDF")]
        public ActionResult _ListadoPDF()
        {
            var turno = db.Turno.Include(t => t.Afiliado).Include(t => t.EspecialidadesProfesional);
            logger.Debug("Se genero un listado de turnos en PDF");
            return View(turno.ToList());
        }

        [Authorize]
        [HttpGet]
        public ActionResult DescargarListadoPDF() => new ViewAsPdf(@"_ListadoPDF", db.Turno)
        {
            FileName = "Listado-Turnos-Medicos-" + DateTime.Today + ".pdf",
            PageSize = Rotativa.Options.Size.A4,
            PageOrientation = Rotativa.Options.Orientation.Landscape,
            CustomSwitches = "--footer-center \"  Creado: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "  Pagina: [page]/[toPage]\"" +
          " --footer-line --footer-font-size \"12\" --footer-spacing 1 --footer-font-name \"Segoe UI\"",
            PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 }
        };
        [ValidateInput(false)]
        // GET: Turno/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.EspecialidadProfesionalId = new SelectList(db.EspecialidadesProfesional.Include(e => e.Especialidad).Include(e => e.Profesional).ToList().Select(o => new { o.Id, Especialidad = string.Format("{0} ({1} {2})", o.Especialidad.descripcion, o.Profesional.Nombre, o.Profesional.Apellido) }).ToList(), "Id", "Especialidad");
            ViewBag.reserva = new SelectList(db.TipoReseva.ToList(), "Id", "descripcion");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turno.Find(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }
        [ValidateInput(false)]
        // GET: Turno/Create
        public ActionResult Create()
        {
            ViewBag.AfiliadoId = new SelectList(db.Afiliado, "Id", "Nombre");
            ViewBag.EspecialidadProfesionalId = new SelectList(db.EspecialidadesProfesional.Include(e => e.Especialidad).Include(e => e.Profesional).ToList().Select(o => new { o.Id, Especialidad = string.Format("{0} ({1} {2})", o.Especialidad.descripcion, o.Profesional.Nombre, o.Profesional.Apellido) }).ToList(), "Id", "Especialidad");
            ViewBag.reserva = new SelectList(db.TipoReseva.ToList(), "Id", "descripcion");
            logger.Debug("Se creo un turno");
            return View();
        }

        // POST: Turno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,Hora,AfiliadoId,EspecialidadProfesionalId,reserva,Observaciones,createdon,createdby,changedon,changedby,deletedon,deletedby,isdeleted")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                db.Turno.Add(turno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Afiliado afiliado = db.Afiliado.Include(a => a.EstadoCivil).Include(a => a.Plan).Include(a => a.TipoDocumento).Include(a => a.TipoSexo).Where(a => a.isdeleted == false).ToList().Where(o => o.Email == User.Identity.Name).FirstOrDefault();
            ViewBag.AfiliadoLogueadoNombreApellido = string.Format("{0} {1} Nº {2} ({3} {4})", afiliado.Nombre, afiliado.Apellido, afiliado.NumeroAfiliado, afiliado.TipoDocumento.descripcion, afiliado.Numero); ;
            ViewBag.AfiliadoId = new SelectList(db.Afiliado, "Id", "Nombre", turno.AfiliadoId);
            ViewBag.EspecialidadProfesionalId = new SelectList(db.EspecialidadesProfesional.Include(e => e.Especialidad).Include(e => e.Profesional).ToList().Select(o => new { o.Id, Especialidad = string.Format("{0} ({1} {2})", o.Especialidad.descripcion, o.Profesional.Nombre, o.Profesional.Apellido) }).ToList(), "Id", "Especialidad");
            ViewBag.reserva = new SelectList(db.TipoReseva.ToList(), "Id", "descripcion");
            logger.Debug("Se creo un turno");
            return View(turno);
        }
        [ValidateInput(false)]
        // GET: Turno/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turno.Find(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            ViewBag.AfiliadoId = new SelectList(db.Afiliado, "Id", "Nombre", turno.AfiliadoId);
            ViewBag.EspecialidadProfesionalId = new SelectList(db.EspecialidadesProfesional.Include(e => e.Especialidad).Include(e => e.Profesional).ToList().Select(o => new { o.Id, Especialidad = string.Format("{0} ({1} {2})", o.Especialidad.descripcion, o.Profesional.Nombre, o.Profesional.Apellido) }).ToList(), "Id", "Especialidad");
            ViewBag.reserva = new SelectList(db.TipoReseva.ToList(), "Id", "descripcion");
            return View(turno);
        }

        // POST: Turno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,Hora,AfiliadoId,EspecialidadProfesionalId,reserva,Observaciones,createdon,createdby,changedon,changedby,deletedon,deletedby,isdeleted")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AfiliadoId = new SelectList(db.Afiliado, "Id", "Nombre", turno.AfiliadoId);
            ViewBag.EspecialidadProfesionalId = new SelectList(db.EspecialidadesProfesional.Include(e => e.Especialidad).Include(e => e.Profesional).ToList().Select(o => new { o.Id, Especialidad = string.Format("{0} ({1} {2})", o.Especialidad.descripcion, o.Profesional.Nombre, o.Profesional.Apellido) }).ToList(), "Id", "Especialidad");
            ViewBag.reserva = new SelectList(db.TipoReseva.ToList(), "Id", "descripcion");
            return View(turno);
        }
        [ValidateInput(false)]
        // GET: Turno/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turno.Find(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            logger.Debug("Se elimino un turno");
            return View(turno);
        }

        // POST: Turno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Turno turno = db.Turno.Find(id);
            db.Turno.Remove(turno);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            logger.Debug("Se elimino un turno");
            base.Dispose(disposing);
        }
    }
}
