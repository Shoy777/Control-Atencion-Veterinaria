namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Rol")]
    public partial class Rol
    {
        public Rol()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int RolId { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }

        public List<Rol> GetAllRoles()
        {
            List<Rol> roles = new List<Rol>();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    roles = context.Rol
                        .OrderBy(x => x.RolId)
                        .ToList();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return roles;
        }

    }

}
