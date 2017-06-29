namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("TipoMedicamento")]
    public partial class TipoMedicamento
    {
        public TipoMedicamento()
        {
            Medicamento = new HashSet<Medicamento>();
        }

        public int TipoMedicamentoId { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public virtual ICollection<Medicamento> Medicamento { get; set; }

        public List<TipoMedicamento> GetAllTipoMedicamento()
        {
            List<TipoMedicamento> tipoMedicamento = new List<TipoMedicamento>();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    tipoMedicamento = context.TipoMedicamento
                        .ToList();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return tipoMedicamento;
        }
    }
}
