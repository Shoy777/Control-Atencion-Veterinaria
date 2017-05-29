namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Diagnostico")]
    public partial class Diagnostico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Diagnostico()
        {
            Receta = new HashSet<Receta>();
        }

        public int DiagnosticoId { get; set; }

        public int VeterinarioId { get; set; }

        public int ConsultaId { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string DiagnosticoDes { get; set; }

        public DateTime FechaDiagnostico { get; set; }

        public virtual Consulta Consulta { get; set; }

        public virtual Usuario Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Receta> Receta { get; set; }
    }
}
