using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace VeterinariaMVC5.Controllers
{
    public class MascotaController : Controller
    {
        Mascota mascota = new Mascota();
        Especie especie = new Especie();
        Cliente cliente = new Cliente();
        Raza raza = new Raza();

        public ActionResult Index()
        {
            return View(mascota.GetAllMascotas());
        }
        public ActionResult Info(int id)
        {
            return View(mascota.GetMascota(id));
        }

        public JsonResult GetAllRazasByEspecie(int id)
        {
            return Json(new SelectList(raza.GetAllRazas(id), "RazaId", "Descripcion"));
        }

        public ActionResult Crud(int id = 0)
        {
            ViewBag.Clientes = new SelectList(cliente.GetAllClientes(), "ClienteId", "NombreCompleto");
            ViewBag.Especies = new SelectList(especie.GetAllEspecies(), "EspecieId", "Descripcion");
            ViewBag.Razas = new SelectList(raza.GetAllRazas(), "RazaId", "Descripcion");
            return View(id > 0 ? mascota.GetMascota(id) : mascota);
        }

        [HttpPost]
        public ActionResult Crud(Mascota m)
        {
            if (ModelState.IsValid)
            {
                m.CrudMascota();
                return Redirect("~/mascota/");
            }
            else
            {
                ViewBag.Clientes = new SelectList(cliente.GetAllClientes(), "ClienteId", "Apellido Nombre");
                ViewBag.Especies = new SelectList(especie.GetAllEspecies(), "EspecieId", "Descripcion");
                ViewBag.Razas = new SelectList(raza.GetAllRazas(), "RazaId", "Descripcion");
                m.Message = "No se ha podido registrar mascota";
                return View("~/views/mascota/crud.cshtml", m);
            }
        }
	}
}