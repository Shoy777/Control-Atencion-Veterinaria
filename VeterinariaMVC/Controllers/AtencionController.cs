using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace VeterinariaMVC.Controllers
{
    public class AtencionController : Controller
    {
        Diagnostico diagnostico = new Diagnostico();

        Consulta consulta = new Consulta();

        //
        // GET: /Atencion/
        public ActionResult Index()
        {
            //return View(diagnostico.GetAllDiagnosticos());
            VeterinariaBDContext context = new VeterinariaBDContext();

            var data = (from d in context.Diagnostico
                        join c in context.Consulta on d.ConsultaId equals c.ConsultaId
                        join u in context.Usuario on d.VeterinarioId equals u.UsuarioId
                        join cli in context.Cliente on c.ClienteId equals cli.ClienteId
                        join m in context.Mascota on c.MascotaId equals m.MascotaId
                        select new DiagnosticoList
                        {
                            ClienteNombre = cli.Nombre,
                            ClienteApellido = cli.Apellido,
                            VeterinarioNombre = u.Nombre,
                            VeterinarioApellido = u.Apellido,
                            FechaDiagnostico = d.FechaDiagnostico,
                            DiagnosticoId = d.DiagnosticoId,
                            ConsultaId = d.ConsultaId,
                            Mascota = m.Nombre
                        });
            return View(data.ToList());
        }

        public ActionResult Listado()
        {
            return View();
        }

        public ActionResult Registrar(int id = 0)
        {
            if (consulta.GetConsultaId(id) != 0)
            {
                diagnostico.ConsultaId = consulta.GetConsultaId(id);
                return View(diagnostico);
            }
            else
            {
                return View("~/consulta/");
            }
        }

        [HttpPost]
        public ActionResult Registrar(Diagnostico d)
        {
            /*
            if (r != null)
            {
                foreach (var c in r)
                    d.Receta.Add(new Receta { MedicamentoId = c.MedicamentoId, Cantidad = c.Cantidad, UnitMedida = c.UnitMedida });
            }
            else
            {
                ModelState.AddModelError("medicamentos", "Debe seleccionar por lo menos un medicamento");
            }
            */
            if (ModelState.IsValid)
            {
                d.registrarDiagnostico(UsuarioController.Get().UsuarioId, d.ConsultaId);
                return View("~/atencion/");
            }else
            return View(d);
        }
	}
}