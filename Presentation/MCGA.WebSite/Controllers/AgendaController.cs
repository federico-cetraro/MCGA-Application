using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MCGA.Data;

namespace MCGA.WebSite.Controllers
{
    [Authorize]
    public class AgendaController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        public JsonResult GetFechasDisponiblesByIdEspecialidadProfesional(int idEspecialidadProfesional = 0)
        {
            List<Agenda> listAgenda = db.Agenda.Include(a => a.EspecialidadesProfesional).Include(a => a.TipoDia).Where(a => a.isdeleted == false).ToList().Where(o => o.EspecialidadProfesionalId == idEspecialidadProfesional && o.AgendaCancelacion.Count == 0 && o.fecha_hasta>=DateTime.UtcNow).ToList();
            if (listAgenda.Count > 0)
            {
                List<string> listaFecha = new List<string>();
                foreach (Agenda agenda in listAgenda)
                {
                    DateTime fecha = DateTime.Now.Date;// agenda.fecha_desde;
                                        
                    while (fecha <= agenda.fecha_hasta)
                    {
                        //Ver si atiende ese dia
                        if (fecha.DayOfWeek.ToString().ToUpper() == agenda.TipoDia.descripcion.ToString().ToUpper())
                        {
                            //Cantidad de horas que atiende en el día
                            int cantidadHoras =
                                (
                                    new DateTime(fecha.Year, fecha.Month, fecha.Day, agenda.hora_hasta.Hours, agenda.hora_hasta.Minutes, 0)
                                    -
                                    new DateTime(fecha.Year, fecha.Month, fecha.Day, agenda.hora_desde.Hours, agenda.hora_desde.Minutes, 0)
                                ).Hours;

                            //Verificar si ese día ya tiene todo agendado
                            if (db.Turno.Include(t => t.Afiliado).Include(t => t.EspecialidadesProfesional).Where(t => t.isdeleted == false).ToList().Where(o => o.Fecha == fecha && o.EspecialidadProfesionalId == idEspecialidadProfesional).Count() != cantidadHoras)
                            {
                                listaFecha.Add(fecha.ToString("yyyy-MM-dd"));
                            }
                        }
                        fecha = fecha.AddDays(1);
                    }
                }
                return Json(listaFecha, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHorarioDisponiblesByIdEspecialidadProfesionalFecha(int idEspecialidadProfesional, DateTime fecha)
        {
            int tipoDiaId = db.TipoDia.ToList().Where(o => o.descripcion.ToUpper() == fecha.DayOfWeek.ToString().ToUpper()).FirstOrDefault().Id;
            Agenda agenda = db.Agenda.Include(a => a.EspecialidadesProfesional).Include(a => a.TipoDia).Where(a => a.isdeleted == false).ToList().Where(o => o.EspecialidadProfesionalId == idEspecialidadProfesional && o.TipoDiaId == tipoDiaId && fecha >= o.fecha_desde && fecha <= o.fecha_hasta && o.AgendaCancelacion.Count == 0).FirstOrDefault();
            if (agenda != null)
            {
                List<dynamic> listaHora = new List<dynamic>();
                TimeSpan hora = agenda.hora_desde;
                while (hora <= agenda.hora_hasta)
                {
                    //ver si hay turno con esa hora y fecha
                    if (db.Turno.Include(t => t.Afiliado).Include(t => t.EspecialidadesProfesional).Where(t => t.isdeleted == false).ToList().FirstOrDefault(o => o.Fecha == fecha && o.Hora == hora && o.EspecialidadProfesionalId == idEspecialidadProfesional) == null)
                    {
                        listaHora.Add(new
                        {
                            HoraText = hora.ToString(@"hh\:mm"),
                            HoraValue = hora.ToString(@"hh\:mm")
                        });
                    }
                    hora = hora.Add(new TimeSpan(1, 0, 0));
                }
                return Json(listaHora, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(null, JsonRequestBehavior.AllowGet);
        }

        // GET: Agenda
        public ActionResult Index()
        {
            var agenda = db.Agenda.Include(a => a.EspecialidadesProfesional).Include(a => a.TipoDia);
            return View(agenda.ToList());
        }

        // GET: Agenda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = db.Agenda.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // GET: Agenda/Create
        public ActionResult Create()
        {
            ViewBag.EspecialidadProfesionalId = new SelectList(db.EspecialidadesProfesional.Include(e => e.Especialidad).Include(e => e.Profesional).ToList().Select(o => new { o.Id, Especialidad = string.Format("{0} ({1} {2})", o.Especialidad.descripcion, o.Profesional.Nombre, o.Profesional.Apellido) }).ToList(), "Id", "Especialidad");
            ViewBag.TipoDiaId = new SelectList(db.TipoDia, "Id", "descripcion");
            return View();
        }

        // POST: Agenda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EspecialidadProfesionalId,TipoDiaId,fecha_desde,fecha_hasta,hora_desde,hora_hasta,createdon,createdby,changedon,changedby,deletedon,deletedby,isdeleted")] Agenda agenda)
        {
            if (ModelState.IsValid)
            {
                db.Agenda.Add(agenda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EspecialidadProfesionalId = new SelectList(db.EspecialidadesProfesional.Include(e => e.Especialidad).Include(e => e.Profesional).ToList().Select(o => new { o.Id, Especialidad = string.Format("{0} ({1} {2})", o.Especialidad.descripcion, o.Profesional.Nombre, o.Profesional.Apellido) }).ToList(), "Id", "Especialidad");
            ViewBag.TipoDiaId = new SelectList(db.TipoDia, "Id", "descripcion", agenda.TipoDiaId);
            return View(agenda);
        }

        // GET: Agenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = db.Agenda.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.EspecialidadProfesionalId = new SelectList(db.EspecialidadesProfesional.Include(e => e.Especialidad).Include(e => e.Profesional).ToList().Select(o => new { o.Id, Especialidad = string.Format("{0} ({1} {2})", o.Especialidad.descripcion, o.Profesional.Nombre, o.Profesional.Apellido) }).ToList(), "Id", "Especialidad");
            ViewBag.TipoDiaId = new SelectList(db.TipoDia, "Id", "descripcion", agenda.TipoDiaId);
            return View(agenda);
        }

        // POST: Agenda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EspecialidadProfesionalId,TipoDiaId,fecha_desde,fecha_hasta,hora_desde,hora_hasta,createdon,createdby,changedon,changedby,deletedon,deletedby,isdeleted")] Agenda agenda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agenda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EspecialidadProfesionalId = new SelectList(db.EspecialidadesProfesional.Include(e => e.Especialidad).Include(e => e.Profesional).ToList().Select(o => new { o.Id, Especialidad = string.Format("{0} ({1} {2})", o.Especialidad.descripcion, o.Profesional.Nombre, o.Profesional.Apellido) }).ToList(), "Id", "Especialidad");
            ViewBag.TipoDiaId = new SelectList(db.TipoDia, "Id", "descripcion", agenda.TipoDiaId);
            return View(agenda);
        }

        // GET: Agenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = db.Agenda.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // POST: Agenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agenda agenda = db.Agenda.Find(id);
            db.Agenda.Remove(agenda);
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
