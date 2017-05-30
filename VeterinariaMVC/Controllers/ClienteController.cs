using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Text.RegularExpressions;

namespace VeterinariaMVC5.Controllers
{
    public class ClienteController : Controller
    {
        //
        // GET: /Cliente/
        Cliente cliente = new Cliente();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Crud(int id = 0)
        {
            return View(id > 0 ? cliente.GetCliente(id) : cliente);
        }

        [HttpPost]
        public JsonResult Crud(Cliente c)
        {
            var cliente = new Cliente
            {
                Nombre = "",
                Apellido = "",
                Telefono = "",
                Message = "",
            };

            /*
            string espacioBlanco = @"(\S)";
            Regex regex = new Regex(espacioBlanco);
            if (!regex.IsMatch(c.Nombre)) cliente.Nombre = "No puede empezar el nombre con espacios en blanco";
            */
            
            if (string.IsNullOrEmpty(c.Nombre)) cliente.Nombre = "Aun no ha ingresado un nombre";
            else if (c.Nombre.Length > 50) cliente.Nombre = "El nombre es muy largo";
            else if (c.Nombre.Length < 2) cliente.Nombre = "El nombre es muy corto";

            if (string.IsNullOrEmpty(c.Apellido)) cliente.Apellido = "Aun no ha ingresado un apellido";
            else if (c.Apellido.Length > 50) cliente.Apellido = "El apellido es muy largo";
            else if (c.Apellido.Length < 2) cliente.Apellido = "El apellido es muy corto";

            if (string.IsNullOrEmpty(c.Telefono)) cliente.Telefono = "Aun no ha ingresado un telefono";
            else if (c.Telefono.Length > 12) cliente.Telefono = "El telefono es muy largo";
            else if (c.Telefono.Length < 7) cliente.Telefono = "El telefono es muy corto";

            if (ModelState.IsValid)
            {
                c.CrudCliente();
                cliente.Message = "Registro guardado";
            }
            return Json(cliente);
        }

        public JsonResult GetAllClientesByLast()
        {
            return Json(new SelectList(cliente.GetAllClientesByLast(), "ClienteId", "NombreCompleto"));
        }
	}
}