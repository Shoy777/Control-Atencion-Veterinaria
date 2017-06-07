using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace VeterinariaMVC.Controllers
{
    public class SearchController : Controller
    {

        Raza raza = new Raza();
        Medicamento medicamento = new Medicamento();
        Mascota mascota = new Mascota();

        public ActionResult Index()
        {
            var lstMedicamentos = medicamento.GetAllMedicamentos().ToList();

            return View(lstMedicamentos);
        }

        public ActionResult Medicamentos(string search_query_top)
        {
            var lstMedicamentos = medicamento.GetAllMedicamentos().Where(x => x.Descripcion.Contains(search_query_top)); ; ; ; ;
            return View("Index", lstMedicamentos);
        }

        public ActionResult Razas(string search_query_top)
        {
            var lstRazas = raza.GetAllRazas().Where(x => x.Descripcion.Contains(search_query_top));
            return View("Index", lstRazas);
        }

        public ActionResult Mascotas(string search_query_top)
        {
            var lstMascotas = mascota.GetAllMascotas().Where(x => x.Nombre.Contains(search_query_top) || x.Raza.Descripcion.Contains(search_query_top));
            return View("Index", lstMascotas);
        }
	}
}