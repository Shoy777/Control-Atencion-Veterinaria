using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

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
            if (ModelState.IsValid)
            {
                c.CrudCliente();
            }
            else
            {
                if (string.IsNullOrEmpty(c.Nombre))
                {
                    c.Nombre = "Aun no ha ingresado un nombre";
                }
                if (string.IsNullOrEmpty(c.Apellido))
                {
                    c.Apellido = "Aun no ha ingresado un apellido";
                }
                if (string.IsNullOrEmpty(c.Telefono))
                {
                    c.Telefono = "Aun no ha ingresado un telefono";
                }
            }
            return Json(c);
        }
	}
}