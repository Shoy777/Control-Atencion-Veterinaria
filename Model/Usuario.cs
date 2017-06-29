namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;
    using System.Web.Security;

    [Table("Usuario")]
    public partial class Usuario
    {
        public Usuario()
        {
            Consulta = new HashSet<Consulta>();
            Diagnostico = new HashSet<Diagnostico>();
        }

        public int UsuarioId { get; set; }

        [Required]
        public int RolId { get; set; }

        [Required(ErrorMessage = "Ingrese Nombre")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese Apellido")]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingrese Telefono")]
        [Phone]
        [StringLength(12)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Ingrese DNI")]
        [StringLength(8)]
        public string DNI { get; set; }

        [Required(ErrorMessage = "Ingrese Nombre de Usuario")]
        [StringLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Seleccione un turno")]
        public byte Turno { get; set; }
        [Required]
        public byte Estado { get; set; }
        public virtual ICollection<Consulta> Consulta { get; set; }
        public virtual ICollection<Diagnostico> Diagnostico { get; set; }
        public virtual Rol Rol { get; set; }

        [NotMapped]
        [Display(Name = "¿Recordar cuenta?")]
        public bool RememberMe { get; set; }

        public List<Usuario> GetAllUsuarios(int estado)
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    usuarios = context.Usuario
                        .Include("Rol")
                        .OrderByDescending(x => x.UsuarioId)
                        .Where(x => x.Estado == estado)
                        .ToList();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return usuarios;
        }

        public Usuario GetUsuario(int id)
        {
            Usuario usuario = new Usuario();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    usuario = context.Usuario
                        .Include("Rol")
                        .Where(x => x.UsuarioId == id)
                        .Single();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return usuario;
        }

        public void CrudUsuario()
        {
            using (var context = new VeterinariaBDContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.UsuarioId == 0)
                        {
                            context.Entry(this).Entity.Estado = 1;//Activo = 1, Eliminado = 0
                            context.Entry(this).Entity.Password = "123456";//Clave por defecto -- Luego el usuario puede modificarlo

                            context.Entry(this).State = EntityState.Added;
                        }
                        else
                        {
                            context.Configuration.AutoDetectChangesEnabled = false;
                            context.Configuration.ValidateOnSaveEnabled = false;

                            context.Entry(this).State = EntityState.Modified;

                            context.Entry(this).Property(x => x.Password).IsModified = false;
                            context.Entry(this).Property(x => x.Estado).IsModified = false;

                        }
                        context.SaveChanges();
                        transaction.Commit();

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        public Usuario Login(string user, string password)
        {
            Usuario usuario = new Usuario();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    usuario = context.Usuario
                        .Include("Rol")
                        .Where(x => x.NombreUsuario == user && x.Password == password)
                        .FirstOrDefault();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return usuario;
        }

        public int CambiarPassword(string Password, string NewPassword, int UsuarioId)
        {
            int usu = 0;

            using (var context = new VeterinariaBDContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                     usu = context.Database.ExecuteSqlCommand(
                               "UPDATE Usuario set Password = '" + NewPassword + "' WHERE Password = @Password AND UsuarioId = @UsuarioId",
                               new SqlParameter("Password", Password),
                               new SqlParameter("UsuarioId", UsuarioId)
                           );

                        context.Configuration.ValidateOnSaveEnabled = false;
                        
                        context.SaveChanges();
                        transaction.Commit();

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new Exception(e.Message);
                    }
                }
            }
            return usu;
        }

        /*
         * Eliminar 0 -- Eliminado
         * Restaurar 1 -- Activo
         */
        public int CambiarEstado(int estado, int UsuarioId)
        {
            int usu = 0;

            using (var context = new VeterinariaBDContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        usu = context.Database.ExecuteSqlCommand(
                                  "UPDATE Usuario set Estado = " + estado + " WHERE UsuarioId = @UsuarioId",
                                  new SqlParameter("UsuarioId", UsuarioId)
                              );

                        context.Configuration.ValidateOnSaveEnabled = false;

                        context.SaveChanges();
                        transaction.Commit();

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new Exception(e.Message);
                    }
                }
            }
            return usu;
        }

    }

    public class CambiarPassword
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar la nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

    }

}