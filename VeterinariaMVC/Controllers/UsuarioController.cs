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
        //
        // GET: /Usuario/
        Usuario usuario = new Usuario();
        Rol rol = new Rol();

        public ActionResult Index()
        {
            return View(usuario.GetAllUsuarios());
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
            if (ModelState.IsValid)
            {
                model.CrudUsuario();
                return Redirect("~/usuario/");
            }
            else
            {
                ViewBag.Roles = new SelectList(rol.GetAllRoles(), "RolId", "Descripcion");
                model.Message = "No se ha podido registrar usuario";
                return View("~/views/usuario/registrar.cshtml", model);
            }
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