namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Consulta")]
    public partial class Consulta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Consulta()
        {
            Diagnostico = new HashSet<Diagnostico>();
        }

        public int ConsultaId { get; set; }

        public int CajeroId { get; set; }

        public int ClienteId { get; set; }

        public int MascotaId { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaAtencion { get; set; }

        public byte Turno { get; set; }

        public decimal Precio { get; set; }

        public byte? Estado { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Mascota Mascota { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diagnostico> Diagnostico { get; set; }
    }
}
