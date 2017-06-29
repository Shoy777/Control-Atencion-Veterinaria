using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace VeterinariaMVC.Controllers
{
    public class MedicamentoController : Controller
    {
        Medicamento medicamento = new Medicamento();
        Especie especie = new Especie();
        Laboratorio laboratorio = new Laboratorio();
        TipoMedicamento tipoMedicamento = new TipoMedicamento();

        public ActionResult Index()
        {
            return View(medicamento.GetAllMedicamentos());
        }
        public ActionResult Info(int id)
        {
            return View(medicamento.GetMedicamento(id));
        }

        public ActionResult Crud(int id = 0)
        {
            ViewBag.TipoMedicamentos = new SelectList(tipoMedicamento.GetAllTipoMedicamento(), "TipoMedicamentoId", "Descripcion");
            ViewBag.Especies = new SelectList(especie.GetAllEspecies(), "EspecieId", "Descripcion");
            ViewBag.Laboratorios = new SelectList(laboratorio.GetAllLaboratorios(), "LaboratorioId", "Descripcion");
            return View(id > 0 ? medicamento.GetMedicamento(id) : medicamento);
        }

        [HttpPost]
        public ActionResult Crud(Medicamento m)
        {
            if (ModelState.IsValid)
            {
                m.CrudMedicamento();
                return Redirect("~/medicamento/");
            }
            else
            {
                ViewBag.TipoMedicamentos = new SelectList(tipoMedicamento.GetAllTipoMedicamento(), "TipoMedicamentoId", "Descripcion");
                ViewBag.Especies = new SelectList(especie.GetAllEspecies(), "EspecieId", "Descripcion");
                ViewBag.Laboratorios = new SelectList(laboratorio.GetAllLaboratorios(), "LaboratorioId", "Descripcion");
                ViewBag.Message = "No se ha podido registrar medicamento";
                return View("~/views/medicamento/crud.cshtml", m);
            }
        }
	}
}