namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetalleBoleta")]
    public partial class DetalleBoleta
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BoletaId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MedicamentoId { get; set; }

        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        public decimal SubTotal { get; set; }

        public virtual Boleta Boleta { get; set; }

        public virtual Medicamento Medicamento { get; set; }
    }
}
