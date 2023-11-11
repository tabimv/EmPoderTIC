﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmPoderTIC.Models;

namespace EmPoderTIC.Controllers
{
    public class OtorgarInsigniaPerfil2Controller : Controller
    {
        private EmPoderTIC_WEB db = new EmPoderTIC_WEB();

        // GET: OtorgarInsigniaPerfil2
        public async Task<ActionResult> Index()
        {
            var oTORGAR_INSIGNIA_P2 = db.OTORGAR_INSIGNIA_P2.Include(o => o.EVENTO).Include(o => o.USUARIO);
            return View(await oTORGAR_INSIGNIA_P2.ToListAsync());
        }

        // GET: OtorgarInsigniaPerfil2/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OTORGAR_INSIGNIA_P2 oTORGAR_INSIGNIA_P2 = await db.OTORGAR_INSIGNIA_P2.FindAsync(id);
            if (oTORGAR_INSIGNIA_P2 == null)
            {
                return HttpNotFound();
            }
            return View(oTORGAR_INSIGNIA_P2);
        }

        // GET: OtorgarInsigniaPerfil2/Create
        public ActionResult Create()
        {
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre");
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre");
            return View();
        }

        // POST: OtorgarInsigniaPerfil2/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "registro_insignia_evento,contribucion_evento,fecha_registro_otorgamiento,USUARIO_rut,EVENTO_evento_id")] OTORGAR_INSIGNIA_P2 oTORGAR_INSIGNIA_P2)
        {
            if (ModelState.IsValid)
            {
                db.OTORGAR_INSIGNIA_P2.Add(oTORGAR_INSIGNIA_P2);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", oTORGAR_INSIGNIA_P2.EVENTO_evento_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", oTORGAR_INSIGNIA_P2.USUARIO_rut);
            return View(oTORGAR_INSIGNIA_P2);
        }

        // GET: OtorgarInsigniaPerfil2/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OTORGAR_INSIGNIA_P2 oTORGAR_INSIGNIA_P2 = await db.OTORGAR_INSIGNIA_P2.FindAsync(id);
            if (oTORGAR_INSIGNIA_P2 == null)
            {
                return HttpNotFound();
            }
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", oTORGAR_INSIGNIA_P2.EVENTO_evento_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", oTORGAR_INSIGNIA_P2.USUARIO_rut);
            return View(oTORGAR_INSIGNIA_P2);
        }

        // POST: OtorgarInsigniaPerfil2/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_insignia_evento,contribucion_evento,fecha_registro_otorgamiento,USUARIO_rut,EVENTO_evento_id")] OTORGAR_INSIGNIA_P2 oTORGAR_INSIGNIA_P2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oTORGAR_INSIGNIA_P2).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", oTORGAR_INSIGNIA_P2.EVENTO_evento_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", oTORGAR_INSIGNIA_P2.USUARIO_rut);
            return View(oTORGAR_INSIGNIA_P2);
        }

        // GET: OtorgarInsigniaPerfil2/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OTORGAR_INSIGNIA_P2 oTORGAR_INSIGNIA_P2 = await db.OTORGAR_INSIGNIA_P2.FindAsync(id);
            if (oTORGAR_INSIGNIA_P2 == null)
            {
                return HttpNotFound();
            }
            return View(oTORGAR_INSIGNIA_P2);
        }

        // POST: OtorgarInsigniaPerfil2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            OTORGAR_INSIGNIA_P2 oTORGAR_INSIGNIA_P2 = await db.OTORGAR_INSIGNIA_P2.FindAsync(id);
            db.OTORGAR_INSIGNIA_P2.Remove(oTORGAR_INSIGNIA_P2);
            await db.SaveChangesAsync();
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
