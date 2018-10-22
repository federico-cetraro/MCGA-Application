﻿using System;
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
    public class EstadoCivilController : Controller
    {
        private MedicureContexto db = new MedicureContexto();

        // GET: EstadoCivil
        public ActionResult Index()
        {
            return View(db.EstadoCivil.ToList());
        }

        // GET: EstadoCivil/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCivil estadoCivil = db.EstadoCivil.Find(id);
            if (estadoCivil == null)
            {
                return HttpNotFound();
            }
            return View(estadoCivil);
        }

        // GET: EstadoCivil/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoCivil/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,descripcion")] EstadoCivil estadoCivil)
        {
            if (ModelState.IsValid)
            {
                db.EstadoCivil.Add(estadoCivil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoCivil);
        }

        // GET: EstadoCivil/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCivil estadoCivil = db.EstadoCivil.Find(id);
            if (estadoCivil == null)
            {
                return HttpNotFound();
            }
            return View(estadoCivil);
        }

        // POST: EstadoCivil/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,descripcion")] EstadoCivil estadoCivil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoCivil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoCivil);
        }

        // GET: EstadoCivil/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCivil estadoCivil = db.EstadoCivil.Find(id);
            if (estadoCivil == null)
            {
                return HttpNotFound();
            }
            return View(estadoCivil);
        }

        // POST: EstadoCivil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoCivil estadoCivil = db.EstadoCivil.Find(id);
            db.EstadoCivil.Remove(estadoCivil);
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
