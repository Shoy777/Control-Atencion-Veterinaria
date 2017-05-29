namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Receta")]
    public partial class Receta
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DiagnosticoId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MedicamentoId { get; set; }

        public int Cantidad { get; set; }

        [StringLength(50)]
        public string UnitMedida { get; set; }

        public virtual Diagnostico Diagnostico { get; set; }

        public virtual Medicamento Medicamento { get; set; }
    }
}
