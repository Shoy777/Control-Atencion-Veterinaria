namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Especie")]
    public partial class Especie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Especie()
        {
            Mascota = new HashSet<Mascota>();
            Medicamento = new HashSet<Medicamento>();
            Raza = new HashSet<Raza>();
        }

        public int EspecieId { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mascota> Mascota { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medicamento> Medicamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Raza> Raza { get; set; }

        [NotMapped]
        public string Message { get; set; }

        public List<Especie> GetAllEspecies()
        {
            List<Especie> especies = new List<Especie>();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    especies = context.Especie
                        .ToList();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return especies;
        }
    }
}
