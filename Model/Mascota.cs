namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Mascota")]
    public partial class Mascota
    {
        public Mascota()
        {
            Consulta = new HashSet<Consulta>();
        }

        public int MascotaId { get; set; }

        public int ClienteId { get; set; }

        public int EspecieId { get; set; }

        public int RazaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }

        public byte Estado { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<Consulta> Consulta { get; set; }

        public virtual Especie Especie { get; set; }

        public virtual Raza Raza { get; set; }

        [NotMapped]
        public string Message { get; set; }

        public List<Mascota> GetAllMascotas()
        {
            List<Mascota> mascotas = new List<Mascota>();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    mascotas = context.Mascota
                        .Include("Cliente")
                        .Include("Especie")
                        .Include("Raza")
                        .ToList();
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }

            return mascotas;
        }
        public Mascota GetMascota(int id)
        {
            Mascota mascota = new Mascota();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    mascota = context.Mascota
                        .Include("Cliente")
                        .Include("Especie")
                        .Include("Raza")
                        .Where(x => x.MascotaId == id)
                        .Single();
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }
            return mascota;
        }

        public void CrudMascota()
        {
            using (var context = new VeterinariaBDContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.MascotaId == 0)
                        {
                            context.Entry(this).Entity.Estado = 1;//Vivo = 1, Fallecido = 0, Enfermo = 2
                            context.Entry(this).State = EntityState.Added;
                        }
                        else
                        {
                            context.Entry(this).State = EntityState.Modified;
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        Message = "Registro guardado";

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Message = e.Message;
                    }
                }
            }
        }

    }
}
