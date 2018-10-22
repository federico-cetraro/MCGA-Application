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
    public class AgendaCancelacioController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        // GET: AgendaCancelacio
        public ActionResult Index()
        {
            var agendaCancelacion = db.AgendaCancelacion.Include(a => a.Agenda);
            return View(agendaCancelacion.ToList());
        }

        // GET: AgendaCancelacio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgendaCancelacion agendaCancelacion = db.AgendaCancelacion.Find(id);
            if (agendaCancelacion == null)
            {
                return HttpNotFound();
            }
            return View(agendaCancelacion);
        }

        // GET: AgendaCancelacio/Create
        public ActionResult Create()
        {
            ViewBag.agenda_id = new SelectList(db.Agenda, "Id", "createdby");
            return View();
        }

        // POST: AgendaCancelacio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,agenda_id,detalle_cancel,fecha_cancel,createdon,createdby,changedon,changedby")] AgendaCancelacion agendaCancelacion)
        {
            if (ModelState.IsValid)
            {
                db.AgendaCancelacion.Add(agendaCancelacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.agenda_id = new SelectList(db.Agenda, "Id", "createdby", agendaCancelacion.agenda_id);
            return View(agendaCancelacion);
        }

        // GET: AgendaCancelacio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgendaCancelacion agendaCancelacion = db.AgendaCancelacion.Find(id);
            if (agendaCancelacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.agenda_id = new SelectList(db.Agenda, "Id", "createdby", agendaCancelacion.agenda_id);
            return View(agendaCancelacion);
        }

        // POST: AgendaCancelacio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,agenda_id,detalle_cancel,fecha_cancel,createdon,createdby,changedon,changedby")] AgendaCancelacion agendaCancelacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agendaCancelacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.agenda_id = new SelectList(db.Agenda, "Id", "createdby", agendaCancelacion.agenda_id);
            return View(agendaCancelacion);
        }

        // GET: AgendaCancelacio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgendaCancelacion agendaCancelacion = db.AgendaCancelacion.Find(id);
            if (agendaCancelacion == null)
            {
                return HttpNotFound();
            }
            return View(agendaCancelacion);
        }

        // POST: AgendaCancelacio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgendaCancelacion agendaCancelacion = db.AgendaCancelacion.Find(id);
            db.AgendaCancelacion.Remove(agendaCancelacion);
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
