using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Model;

namespace VeterinariaMVC.Controllers
{
    public class UsuarioController : Controller
    {
        Usuario usuario = new Usuario();
        Rol rol = new Rol();

        public ActionResult Index()
        {
            /*Rol Cajero*/
            if (UsuarioController.Get().Rol.Descripcion.Equals("Cajero"))
            {
                return Redirect("~/Usuario/Perfil");
            }
            /*Rol Admin y Veterinario*/
            else
                return View(usuario.GetAllUsuarios(1));
        }

        public ActionResult Eliminados()
        {
            /*Rol Cajero*/
            if (UsuarioController.Get().Rol.Descripcion.Equals("Cajero"))
            {
                return Redirect("~/Usuario/Perfil");
            }
            /*Rol Admin y Veterinario*/
            else
                return View(usuario.GetAllUsuarios(0));
        }

        public ActionResult Crud(int id = 0)
        {
            ViewBag.Roles = new SelectList(rol.GetAllRoles(), "RolId", "Descripcion");
            /*Rol Cajero*/
            if (UsuarioController.Get().Rol.Descripcion.Equals("Cajero"))
            {
                if (id == UsuarioController.Get().UsuarioId)
                {
                    return View(usuario.GetUsuario(id));
                }
                else
                {
                    return Redirect("~/Usuario/Perfil");
                }
            }
            /*Rol Admin y Veterinario*/
            else return View(id > 0 ? usuario.GetUsuario(id) : usuario);
        }

        [HttpPost]
        public ActionResult Crud(Usuario model)
        {
            VeterinariaBDContext context = new VeterinariaBDContext();
            var dni = context.Usuario.Where(x => x.DNI == model.DNI).FirstOrDefault();
            var user = context.Usuario.Where(x => x.NombreUsuario == model.NombreUsuario).FirstOrDefault();
            var nombre = context.Usuario.Where(x => x.Nombre == model.Nombre && x.Apellido == model.Apellido).FirstOrDefault();

            ModelState.Remove("Password");
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            /*Editar*/
            if (model.UsuarioId > 0)
            {
                var usu = context.Usuario.Where(x => x.UsuarioId == model.UsuarioId).FirstOrDefault();
                if (dni != null && !model.DNI.Equals(usu.DNI))
                {
                    if (model.DNI.Equals(dni.DNI) && model.UsuarioId != dni.UsuarioId)
                        ViewBag.Message = "Ya existe usuario con el mismo DNI";
                }
                else if (user != null && !model.NombreUsuario.Equals(usu.NombreUsuario))
                {
                    if (model.NombreUsuario.Equals(user.NombreUsuario) && model.UsuarioId != user.UsuarioId)
                        ViewBag.Message = "Ya existe usuario con el mismo Nombre de Usuario";
                }
                else if (nombre != null && !(model.Nombre.Equals(usu.Nombre) && model.Apellido.Equals(usu.Apellido)))
                {
                    if (model.Nombre.Equals(nombre.Nombre) && model.Apellido.Equals(nombre.Apellido) && model.UsuarioId != nombre.UsuarioId)
                        ViewBag.Message = "Ya existe usuario con el mismo Nombre y Apellido";
                }
                else if(ModelState.IsValid)
                {
                    model.CrudUsuario();
                    return Redirect("~/usuario/");
                }
            }
            /*Registrar*/
            else
            {
                if (dni != null && model.UsuarioId == 0)
                {
                    ViewBag.Message = "Ya existe usuario con el mismo DNI";
                }
                else if (user != null && model.UsuarioId == 0)
                {
                    ViewBag.Message = "Ya existe usuario con el mismo Nombre de Usuario";
                }
                else if (nombre != null && model.UsuarioId == 0)
                {
                    ViewBag.Message = "Ya existe usuario con el mismo Nombre y Apellido";
                }
                else if (ModelState.IsValid)
                {
                    model.CrudUsuario();
                    return Redirect("~/usuario/");
                }
            }
            ViewBag.Roles = new SelectList(rol.GetAllRoles(), "RolId", "Descripcion");
            return View("~/views/usuario/crud.cshtml", model);
        }

        public ActionResult Eliminar(int id = 0)
        {
            ViewBag.Roles = new SelectList(rol.GetAllRoles(), "RolId", "Descripcion");

            if (UsuarioController.Get().Rol.Descripcion.Equals("Cajero"))
            {
                return Redirect("~/Usuario/Perfil");
            }
            else
            {
                return View(usuario.GetUsuario(id));
            }
        }

        [HttpPost]
        public ActionResult Eliminar(Usuario model)
        {
            if (model.UsuarioId == UsuarioController.Get().UsuarioId)
            {
                ViewBag.Roles = new SelectList(rol.GetAllRoles(), "RolId", "Descripcion");
                ViewBag.Message = "Usted no puede eliminar su cuenta";
                model = UsuarioController.Get();
                return View("~/views/usuario/eliminar.cshtml", model);
            }
            else
            {
                usuario.CambiarEstado(0, model.UsuarioId);
                ViewBag.Roles = new SelectList(rol.GetAllRoles(), "RolId", "Descripcion");
                return Redirect("~/usuario/eliminados");
            }

        }

        public ActionResult Restaurar(int id = 0)
        {
            ViewBag.Roles = new SelectList(rol.GetAllRoles(), "RolId", "Descripcion");

            if (UsuarioController.Get().Rol.Descripcion.Equals("Cajero"))
            {
                return Redirect("~/Usuario/Perfil");
            }
            else
            {
                return View(usuario.GetUsuario(id));
            }
        }

        [HttpPost]
        public ActionResult Restaurar(Usuario model)
        {
            usuario.CambiarEstado(1, model.UsuarioId);
            ViewBag.Roles = new SelectList(rol.GetAllRoles(), "RolId", "Descripcion");
            return Redirect("~/usuario/");

        }

        public ActionResult CambiarPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarPassword(CambiarPassword user)
        {
            var userpass = usuario.CambiarPassword(user.OldPassword, user.NewPassword, UsuarioController.Get().UsuarioId);
            if (userpass > 0)
            {
                ViewBag.Mensaje = "Se cambio la clave";
                ViewBag.userpass = userpass;
            }
            else
            {
                ViewBag.Mensaje = "La contraseña actual es incorrecta";
                ViewBag.userpass = userpass;
            }

            return View();
        }

        public ActionResult Perfil(){
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                return Redirect("~/");
            }
            else {
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Usuario user)
        {
            usuario = user.Login(user.NombreUsuario,user.Password);

            if (usuario != null)
            {
                if (usuario.Estado == 1)
                {
                    var cookie = FormsAuthentication.GetAuthCookie("usuario", user.RememberMe);

                    cookie.Name = FormsAuthentication.FormsCookieName;

                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    var newTicket = new FormsAuthenticationTicket(1, ticket.Name,
                        ticket.IssueDate, ticket.Expiration, user.RememberMe, usuario.UsuarioId.ToString());

                    cookie.Value = FormsAuthentication.Encrypt(newTicket);
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

                    return Redirect("~/");
                }
                else
                {
                    ViewBag.Message = "Su cuenta ha sido desabilitada";
                    return View(user);
                }
            }
            else
            {
                ViewBag.Message = "Credenciales incorrectas";
                return View(user);
            }

        }

        public static int GetUser()
        {
            int user_id = 0;
            if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)System.Web.HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    user_id = Convert.ToInt32(ticket.UserData);
                }
            }
            return user_id;
        }

        public static Usuario Get()
        {
            return new Usuario().GetUsuario(UsuarioController.GetUser());
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Usuario/Login");
        }
	}
}