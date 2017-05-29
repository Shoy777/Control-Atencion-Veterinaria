namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Raza")]
    public partial class Raza
    {
        public Raza()
        {
            Mascota = new HashSet<Mascota>();
        }

        public int RazaId { get; set; }

        public int EspecieId { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [StringLength(255)]
        public string Imagen { get; set; }

        public virtual Especie Especie { get; set; }

        public virtual ICollection<Mascota> Mascota { get; set; }

        [NotMapped]
        public string Message { get; set; }

        public List<Raza> GetAllRazas()
        {
            List<Raza> razas = new List<Raza>();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    razas = context.Raza
                        .ToList();
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }

            return razas;
        }

        public List<Raza> GetAllRazas(int id)
        {
            List<Raza> razas = new List<Raza>();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    razas = context.Raza
                        .Where(x => x.EspecieId == id)
                        .ToList();
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }

            return razas;
        }

    }
}
