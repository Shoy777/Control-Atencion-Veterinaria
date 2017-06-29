namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Medicamento")]
    public partial class Medicamento
    {
        public Medicamento()
        {
            DetalleBoleta = new HashSet<DetalleBoleta>();
            Receta = new HashSet<Receta>();
        }

        public int MedicamentoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public int EspecieId { get; set; }

        public int TipoMedicamentoId { get; set; }

        public int LaboratorioId { get; set; }

        [Column(TypeName = "text")]
        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public byte? Estado { get; set; }

        public virtual ICollection<DetalleBoleta> DetalleBoleta { get; set; }

        public virtual Especie Especie { get; set; }

        public virtual Laboratorio Laboratorio { get; set; }

        public virtual TipoMedicamento TipoMedicamento { get; set; }

        public virtual ICollection<Receta> Receta { get; set; }

        public Medicamento GetMedicamento(int id)
        {
            Medicamento medicamento = new Medicamento();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    medicamento = context.Medicamento
                        .Include("TipoMedicamento")
                        .Include("Especie")
                        .Include("Laboratorio")
                        .Where(x => x.MedicamentoId == id)
                        .Single();
                }
                catch (Exception e)
                {
                    new Exception(e.Message);
                }
            }
            return medicamento;
        }

        public List<Medicamento> GetAllMedicamentos()
        {
            List<Medicamento> medicamentos = new List<Medicamento>();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    medicamentos = context.Medicamento
                        .Include("TipoMedicamento")
                        .Include("Especie")
                        .Include("Laboratorio")
                        .OrderByDescending(x => x.MedicamentoId)
                        .ToList();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return medicamentos;
        }

        public void CrudMedicamento()
        {
            using (var context = new VeterinariaBDContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.MedicamentoId == 0)
                        {
                            context.Entry(this).Entity.Estado = 1;
                            context.Entry(this).Entity.Stock = 0;
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
