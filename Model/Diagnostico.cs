namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
    using System.Linq;

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

        /*
        public List<Diagnostico> GetAllDiagnosticos()
        {
            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    var data = (from d in context.Diagnostico
                               join c in context.Consulta on d.ConsultaId equals c.ConsultaId
                               join u in context.Usuario on d.VeterinarioId equals u.UsuarioId
                               join cli in context.Cliente on c.ClienteId equals cli.ClienteId
                               join m in context.Mascota on c.MascotaId equals m.MascotaId
                               select new {
                                   Cliente = cli.Nombre+' '+cli.Apellido,
                                   Veterinario = u.Nombre+' '+u.Apellido,
                                   FechaDiagnostico = d.FechaDiagnostico,
                                   DiagnosticoId = d.DiagnosticoId
                               });

                }
                catch (Exception)
                {
                    throw;
                }
            }

            return data;
        }
        */

        public void registrarDiagnostico(int VeterinarioId, int ConsultaId)
        {
            using (var context = new VeterinariaBDContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Entry(this).Entity.FechaDiagnostico = DateTime.Now;
                        context.Entry(this).Entity.VeterinarioId = VeterinarioId;
                        context.Entry(this).Entity.ConsultaId = ConsultaId;

                        context.Entry(this).State = EntityState.Added;
                        /*
                        foreach (var r in this.Receta)
                            context.Entry(r).State = EntityState.Unchanged;
                        */
                        context.Database.ExecuteSqlCommand(
                            "UPDATE Consulta set Estado = 2 WHERE ConsultaId = @ConsultaId",
                            new SqlParameter("ConsultaId", ConsultaId)
                            );

                        transaction.Commit();

                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

    }
}
