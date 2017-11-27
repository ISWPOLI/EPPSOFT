using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EppsoftRecolector.Models;

namespace EppsoftRecolector.Controllers
{
    public class PlanillasController : Controller
    {
        private EPPSOFTRecolectorEntities db = new EPPSOFTRecolectorEntities();

        // GET: Planillas
        public ActionResult Index()
        {
            var planilla = db.Planilla.Include(p => p.ClaseProducto).Include(p => p.NovedadServicio).Include(p => p.Usuario).Include(p => p.Vehiculo);
            return View(planilla.ToList());
        }

        //GET: Planillas/Reporte
        //public JsonResult Reporte()
        //{
        //    var planilla = db.Planilla.Include(p => p.ClaseProducto).Include(p => p.NovedadServicio).Include(p => p.Usuario).Include(p => p.Vehiculo);
        //    mydataservice OBJMD = new mydataservice;
        //    chartsdata = objMD.ListData();
        //    return Json(chartsdata, JsonRequestBehavior.AllowGet);
        //}

        // GET: Planillas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planilla planilla = db.Planilla.Find(id);
            if (planilla == null)
            {
                return HttpNotFound();
            }
            return View(planilla);
        }

        // GET: Planillas/Create
        public ActionResult Create()
        {
            ViewBag.idClaseProducto = new SelectList(db.ClaseProducto, "idClaseProducto", "sCodigoClaseProducto");
            ViewBag.idNovedadesServicio = new SelectList(db.NovedadServicio, "idNovedadServicio", "sCodigoNovedadServicio");
            ViewBag.idGenerador = new SelectList(db.Usuario, "IdUsuario", "NombreUsuario");
            ViewBag.idVehiculoRecoleccion = new SelectList(db.Vehiculo, "idVehiculo", "sPlaca");
            return View();
        }

        // POST: Planillas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPlanilla,iCodigoPlanilla,idGenerador,dFechaRecolección,idVehiculoRecoleccion,iReportadoUsuarioKg,iRecolectadoKg,sRecipientes,bEncontrados,bRecogidos,sNombreUsuarioEntrega,imgFirmaRecolector,imgFirmaUsuario,idNovedadesServicio,sOtrasNovedades,idClaseProducto")] Planilla planilla)
        {
            if (ModelState.IsValid)
            {
                db.Planilla.Add(planilla);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idClaseProducto = new SelectList(db.ClaseProducto, "idClaseProducto", "sCodigoClaseProducto", planilla.idClaseProducto);
            ViewBag.idNovedadesServicio = new SelectList(db.NovedadServicio, "idNovedadServicio", "sCodigoNovedadServicio", planilla.idNovedadesServicio);
            ViewBag.idGenerador = new SelectList(db.Usuario, "IdUsuario", "NombreUsuario", planilla.idGenerador);
            ViewBag.idVehiculoRecoleccion = new SelectList(db.Vehiculo, "idVehiculo", "sPlaca", planilla.idVehiculoRecoleccion);
            return View(planilla);
        }

        // GET: Planillas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planilla planilla = db.Planilla.Find(id);
            if (planilla == null)
            {
                return HttpNotFound();
            }
            ViewBag.idClaseProducto = new SelectList(db.ClaseProducto, "idClaseProducto", "sCodigoClaseProducto", planilla.idClaseProducto);
            ViewBag.idNovedadesServicio = new SelectList(db.NovedadServicio, "idNovedadServicio", "sCodigoNovedadServicio", planilla.idNovedadesServicio);
            ViewBag.idGenerador = new SelectList(db.Usuario, "IdUsuario", "NombreUsuario", planilla.idGenerador);
            ViewBag.idVehiculoRecoleccion = new SelectList(db.Vehiculo, "idVehiculo", "sPlaca", planilla.idVehiculoRecoleccion);
            return View(planilla);
        }

        // POST: Planillas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPlanilla,iCodigoPlanilla,idGenerador,dFechaRecolección,idVehiculoRecoleccion,iReportadoUsuarioKg,iRecolectadoKg,sRecipientes,bEncontrados,bRecogidos,sNombreUsuarioEntrega,imgFirmaRecolector,imgFirmaUsuario,idNovedadesServicio,sOtrasNovedades,idClaseProducto")] Planilla planilla)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planilla).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idClaseProducto = new SelectList(db.ClaseProducto, "idClaseProducto", "sCodigoClaseProducto", planilla.idClaseProducto);
            ViewBag.idNovedadesServicio = new SelectList(db.NovedadServicio, "idNovedadServicio", "sCodigoNovedadServicio", planilla.idNovedadesServicio);
            ViewBag.idGenerador = new SelectList(db.Usuario, "IdUsuario", "NombreUsuario", planilla.idGenerador);
            ViewBag.idVehiculoRecoleccion = new SelectList(db.Vehiculo, "idVehiculo", "sPlaca", planilla.idVehiculoRecoleccion);
            return View(planilla);
        }

        // GET: Planillas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planilla planilla = db.Planilla.Find(id);
            if (planilla == null)
            {
                return HttpNotFound();
            }
            return View(planilla);
        }

        // POST: Planillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Planilla planilla = db.Planilla.Find(id);
            db.Planilla.Remove(planilla);
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
