namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Medicamento")]
    public partial class Medicamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        [StringLength(255)]
        public string Imagen { get; set; }

        public byte? Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleBoleta> DetalleBoleta { get; set; }

        public virtual Especie Especie { get; set; }

        public virtual Laboratorio Laboratorio { get; set; }

        public virtual TipoMedicamento TipoMedicamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Receta> Receta { get; set; }
    }
}
