namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Especie")]
    public partial class Especie
    {
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

        public virtual ICollection<Mascota> Mascota { get; set; }

        public virtual ICollection<Medicamento> Medicamento { get; set; }

        public virtual ICollection<Raza> Raza { get; set; }

        public Especie GetEspecie(int id)
        {
            Especie especie = new Especie();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    especie = context.Especie
                        .Where(x => x.EspecieId == id)
                        .Single();
                }
                catch (Exception e)
                {
                    new Exception(e.Message);
                }
            }
            return especie;
        }

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

        public void CrudEspecie()
        {
            using (var context = new VeterinariaBDContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.EspecieId == 0)
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
