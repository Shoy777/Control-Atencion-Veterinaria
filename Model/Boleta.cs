namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Boleta")]
    public partial class Boleta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Boleta()
        {
            DetalleBoleta = new HashSet<DetalleBoleta>();
        }

        public int BoletaId { get; set; }

        [StringLength(50)]
        public string Cliente { get; set; }

        public decimal MontoTotal { get; set; }

        public int? CantidadItems { get; set; }

        public DateTime FechaVenta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleBoleta> DetalleBoleta { get; set; }
    }
}
