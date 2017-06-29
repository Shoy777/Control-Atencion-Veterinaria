namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
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

        public Raza GetRaza(int id)
        {
            Raza raza = new Raza();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    raza = context.Raza
                        .Include("Especie")
                        .Where(x => x.EspecieId == id)
                        .Single();
                }
                catch (Exception e)
                {
                    new Exception(e.Message);
                }
            }
            return raza;
        }

        public List<Raza> GetAllRazas()
        {
            List<Raza> razas = new List<Raza>();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    razas = context.Raza.Include("Especie")
                        .ToList();
                }
                catch (Exception e)
                {
                    new Exception(e.Message);
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
                    razas = context.Raza.Include("Especie")
                        .Where(x => x.EspecieId == id)
                        .ToList();
                }
                catch (Exception e)
                {
                    new Exception(e.Message);
                }
            }

            return razas;
        }

        public void CrudRaza()
        {
            using (var context = new VeterinariaBDContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.RazaId == 0)
                        {
                            context.Entry(this).State = EntityState.Added;
                        }
                        else
                        {
                            context.Entry(this).State = EntityState.Modified;
                        }
                        context.SaveChanges();
                        transaction.Commit();

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        new Exception(e.Message);
                    }
                }
            }
        }

    }
}
