using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Data.Entity;

namespace VeterinariaMVC.Controllers
{
    public class ConsultaController : Controller
    {

        Cliente cliente = new Cliente();
        Consulta consulta = new Consulta();
        Mascota mascota = new Mascota();

        public ActionResult Index()
        {
            return View(consulta.GetAllConsultasDia());
        }
        public ActionResult Consultas()
        {
            return View(consulta.GetAllConsultas(1));
        }

        public ActionResult Anuladas()
        {
            return View(consulta.GetAllConsultas(0));
        }

        public ActionResult Atendidas()
        {
            return View(consulta.GetAllConsultas(2));
        }


        public ActionResult Anular(int id = 0)
        {
            ViewBag.Clientes = new SelectList(cliente.GetAllClientes(), "ClienteId", "NombreCompleto");
            ViewBag.Mascotas = new SelectList(mascota.GetAllMascotas(), "MascotaId", "Nombre");
            if (consulta.GetConsulta(id)!=null)
            {
                return View(consulta.GetConsulta(id));
            }
            return View();

        }
        
        [HttpPost]
        public ActionResult Anular(Consulta c)
        {
            ViewBag.Clientes = new SelectList(cliente.GetAllClientes(), "ClienteId", "NombreCompleto");
            ViewBag.Mascotas = new SelectList(mascota.GetAllMascotas(), "MascotaId", "Nombre");
            int con = consulta.CambiarEstado(c.ConsultaId, 0);
            if (con > 0)
            {
                if (consulta.Estado == 1)
                {
                    ViewBag.Message = "La Consulta ya ha sido anulada!";
                }
                else
                {
                    c.Message = "Se Anuló la consulta";
                }
                return View(consulta.GetConsulta(c.ConsultaId));
            }
            else
            {
                c.Message = "Ingrese una consulta valida";
                return View(c);
            }
        }

        public ActionResult Restaurar(int id = 0)
        {
            ViewBag.Clientes = new SelectList(cliente.GetAllClientes(), "ClienteId", "NombreCompleto");
            ViewBag.Mascotas = new SelectList(mascota.GetAllMascotas(), "MascotaId", "Nombre");
            if (consulta.GetConsulta(id) != null)
            {
                return View(consulta.GetConsulta(id));
            }
            return View();
        }

        [HttpPost]
        public ActionResult Restaurar(Consulta c)
        {
            ViewBag.Clientes = new SelectList(cliente.GetAllClientes(), "ClienteId", "NombreCompleto");
            ViewBag.Mascotas = new SelectList(mascota.GetAllMascotas(), "MascotaId", "Nombre");
            int con = consulta.CambiarEstado(c.ConsultaId, 1);
            if (con > 0)
            {
                if (consulta.Estado == 1)
                {
                    ViewBag.Message = "La Consulta ya ha sido restaurada!";
                }
                else
                {
                    ViewBag.Message = "Se Restauró la consulta";
                }
                return View(consulta.GetConsulta(c.ConsultaId));
            }
            else
            {
                ViewBag.Message = "Ingrese una consulta valida";
                return View(c);
            }
        }
        
        public ActionResult Crud(int id = 0)
        {
            var c = consulta.GetConsulta(id);
            if (c.ConsultaId > 0)
            {
                ViewBag.NombreCliente = c.Cliente.NombreCompleto;
                ViewBag.NombreMascota = c.Mascota.Nombre;
            }
            return View(id > 0 ? consulta.GetConsulta(id) : consulta);
        }

        [HttpPost]
        public JsonResult Crud(Consulta c)
        {
            var VerificarConsulta = c.VerificarConsulta(c.ClienteId, c.MascotaId, c.FechaAtencion);

            if (ModelState.IsValid)
            {
                if (VerificarConsulta != null)
                {
                    if (VerificarConsulta.ConsultaId != c.ConsultaId)
                    {
                        c.Message = "La Mascota ya tiene una consulta para el mismo dia";
                        c.valor = 3;
                    } else {
                        c.valor = c.CrudConsulta(UsuarioController.Get().UsuarioId);
                        c.Message = "Consulta Modificada";
                        c.valor = 2;
                    }
                }
                else
                {
                    c.valor = c.CrudConsulta(UsuarioController.Get().UsuarioId);
                    c.Message = "Consulta Registrada";
                    c.valor = 1;
                }
                
            }
            else
            {
                if (c.FechaAtencion <= DateTime.Today)
                {
                    c.Message = "Seleccione un Fecha Actual";
                    c.valor = 0;
                }
                else
                {
                    c.Message = "Llene todos los campos";
                    c.valor = 3;
                }
            }
            return Json(c);
        }
    }
}