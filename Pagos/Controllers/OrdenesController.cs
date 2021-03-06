﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pagos.Models;

namespace Pagos.Controllers
{
    public class OrdenesController : Controller
    {
        private nortonEntities db = new nortonEntities();

        // GET: Ordenes
        public ActionResult Index()
        {
            var ordenes = db.Ordenes.Include(o => o.Proveedores);
            return View(ordenes.ToList());
        }

        // GET: Ordenes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordenes ordenes = db.Ordenes.Find(id);
            if (ordenes == null)
            {
                return HttpNotFound();
            }
            return View(ordenes);
        }

        // GET: Ordenes/Create
        public ActionResult Create()
        {
            ViewBag.OrdenProveedor = new SelectList(db.Proveedores, "ProveedorId", "ProveedorRuc");
            return View();
        }

        // POST: Ordenes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrdenId,OrdenCodigo,OrdenTipo,OrdenFecha,OrdenProveedor,OrdenSubtotal,OrdenIgv,OrdenTotal,OrdenPlazoEntrega,OrdenFormaPago,OrdenContactoInterno,OrdenLugarEntrega,OrdenEstado,OrdenObservacion,OrdenMotivoRechazo")] Ordenes ordenes)
        {
            if (ModelState.IsValid)
            {
                ordenes.OrdenId = Guid.NewGuid();
                db.Ordenes.Add(ordenes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrdenProveedor = new SelectList(db.Proveedores, "ProveedorId", "ProveedorRuc", ordenes.OrdenProveedor);
            return View(ordenes);
        }

        // GET: Ordenes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordenes ordenes = db.Ordenes.Find(id);
            if (ordenes == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrdenProveedor = new SelectList(db.Proveedores, "ProveedorId", "ProveedorRuc", ordenes.OrdenProveedor);
            return View(ordenes);
        }

        // POST: Ordenes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrdenId,OrdenCodigo,OrdenTipo,OrdenFecha,OrdenProveedor,OrdenSubtotal,OrdenIgv,OrdenTotal,OrdenPlazoEntrega,OrdenFormaPago,OrdenContactoInterno,OrdenLugarEntrega,OrdenEstado,OrdenObservacion,OrdenMotivoRechazo")] Ordenes ordenes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordenes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrdenProveedor = new SelectList(db.Proveedores, "ProveedorId", "ProveedorRuc", ordenes.OrdenProveedor);
            return View(ordenes);
        }

        // GET: Ordenes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordenes ordenes = db.Ordenes.Find(id);
            if (ordenes == null)
            {
                return HttpNotFound();
            }
            return View(ordenes);
        }

        // POST: Ordenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Ordenes ordenes = db.Ordenes.Find(id);
            db.Ordenes.Remove(ordenes);
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
