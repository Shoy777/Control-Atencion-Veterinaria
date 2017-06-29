namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Laboratorio")]
    public partial class Laboratorio
    {
        public Laboratorio()
        {
            Medicamento = new HashSet<Medicamento>();
        }

        public int LaboratorioId { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public virtual ICollection<Medicamento> Medicamento { get; set; }

        public List<Laboratorio> GetAllLaboratorios()
        {
            List<Laboratorio> laboratorios = new List<Laboratorio>();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    laboratorios = context.Laboratorio
                        .ToList();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return laboratorios;
        }

    }
}
