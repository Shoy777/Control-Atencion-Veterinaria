using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace VeterinariaMVC5.Controllers
{
    public class ClienteController : Controller
    {
        Cliente cliente = new Cliente();
        
        public ActionResult Index()
        {
            return View(cliente.GetAllClientes());
        }

        public ActionResult Info(int id = 0)
        {
            VeterinariaBDContext context = new VeterinariaBDContext();
            var c = context.Cliente.Find(id);
            if (c != null)
            {
                return View(cliente.GetCliente(id));
            }
            else
            {
                return Redirect("~/cliente/");
            }
        }

        public ActionResult Registrar()
        {
            return View(cliente);
        }

        [HttpPost]
        public JsonResult Registrar(Cliente c)
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
            else if (string.IsNullOrEmpty(c.Apellido)) cliente.Apellido = "Aun no ha ingresado un apellido";
            else if (c.Apellido.Length > 50) cliente.Apellido = "El apellido es muy largo";
            else if (c.Apellido.Length < 2) cliente.Apellido = "El apellido es muy corto";
            else if (string.IsNullOrEmpty(c.Telefono)) cliente.Telefono = "Aun no ha ingresado un telefono";
            else if (c.Telefono.Length > 12) cliente.Telefono = "El telefono es muy largo";
            else if (c.Telefono.Length < 7) cliente.Telefono = "El telefono es muy corto";
            else
            {
                if (ModelState.IsValid)
                {
                    VeterinariaBDContext context = new VeterinariaBDContext();

                    var cli = context.Cliente.Where(x => x.Nombre == c.Nombre && x.Apellido == c.Apellido).FirstOrDefault();
                    if (cli != null)
                    {
                        cliente.Message = "Cliente Existe";
                    }
                    else
                    {
                        c.CrudCliente();
                        cliente.Message = "Registro guardado";
                    }
                }
            }
            return Json(cliente);
        }

        public ActionResult Modificar(int id = 0)
        {
            VeterinariaBDContext context = new VeterinariaBDContext();
            var c = context.Cliente.Find(id);
            if (c != null)
            {
                return View(cliente.GetCliente(id));
            }
            else
            {
                return Redirect("~/cliente/");
            }
        }

        [HttpPost]
        public ActionResult Modificar(Cliente c)
        {
            if (ModelState.IsValid)
            {
                VeterinariaBDContext context = new VeterinariaBDContext();
                
                var cli = context.Cliente.Where(x => x.Nombre == c.Nombre && x.Apellido == c.Apellido).FirstOrDefault();
                if (cli != null && (c.ClienteId != cli.ClienteId))
                {
                    c.Message = "Ingrese otro Nombre y Apellido";
                    return View("Modificar", c);
                }
                else
                {
                    c.CrudCliente();
                    c.Message = "Registro modificado";
                    return View("Modificar", c);
                }
            }
            else
            {
                c.Message = "¡Falto algo!";
                return View("Modificar", c);
            }
        }

        public JsonResult GetAllClientesByLast()
        {
            return Json(new SelectList(cliente.GetAllClientesByLast(), "ClienteId", "NombreCompleto"));
        }

        public JsonResult GetClienteByQuery(string query)
        {
            VeterinariaBDContext context = new VeterinariaBDContext();
            
            List<Cliente> cliente = new List<Cliente>();
            
            context.Configuration.ProxyCreationEnabled = false;

                cliente = context.Cliente
                    .Where(x => x.NombreCompleto.Contains(query)).ToList();
            
            return Json(cliente);

        }

        public JsonResult GetMascotasByCliente(int id)
        {
            try
            {
                using (var context = new VeterinariaBDContext())
                {
                    IQueryable<Mascota> mascotas = context.Mascota.Include(x => x.Raza).Where(x => x.ClienteId == id);
                    List<Mascota> Mascotas = new List<Mascota>();
                    foreach (var m in mascotas)
                    {
                        Mascota mascota = new Mascota();
                        mascota.MascotaId = m.MascotaId;
                        mascota.Nombre = m.Nombre;
                        mascota.FechaNacimiento = m.FechaNacimiento;
                        //mascota.Raza.Descripcion = context.Raza.Where(x => x.RazaId == m.RazaId).SingleOrDefault().Descripcion;
                        //mascota.Raza.Descripcion = m.Raza.Descripcion;
                        Mascotas.Add(mascota);
                    }
                    return Json(new { Results = Mascotas }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

	}
}