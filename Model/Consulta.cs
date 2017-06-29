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

    [Table("Consulta")]
    public partial class Consulta
    {
        public Consulta()
        {
            Diagnostico = new HashSet<Diagnostico>();
        }

        public int ConsultaId { get; set; }

        public int CajeroId { get; set; }

        [Range(1,Int32.MaxValue,ErrorMessage="Debe seleccionar un cliente")]
        public int ClienteId { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Debe seleccionar una mascota")]
        public int MascotaId { get; set; }

        public DateTime FechaRegistro { get; set; }

        [CustomDateRange(ErrorMessage="Seleccione una fecha actual")]
        public DateTime FechaAtencion { get; set; }

        public byte Turno { get; set; }

        [Range(11, Double.MaxValue, ErrorMessage = "El precio debe ser mayor a 10")]
        public decimal Precio { get; set; }

        public byte? Estado { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Mascota Mascota { get; set; }

        public virtual ICollection<Diagnostico> Diagnostico { get; set; }

        [NotMapped]
        public string Message { get; set; }

        [NotMapped]
        public int valor { get; set; }

        public int GetConsultaId(int id)
        {

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    id = context.Consulta
                        .Where(x => x.ConsultaId == id)
                        .Single().ConsultaId;
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }
            return id;
        }

        public Consulta GetConsulta(int id)
        {
            Consulta consulta = new Consulta();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    consulta = context.Consulta
                        .Include("Cliente")
                        .Include("Mascota")
                        .Include("Usuario")
                        .Where(x => x.ConsultaId == id)
                        .Single();
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }
            return consulta;
        }

        public List<Consulta> GetAllConsultas(int estado)
        {
            List<Consulta> consultas = new List<Consulta>();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    if (estado == 1)//Por Atender
                        consultas = context.Consulta.Include("Cliente").Include("Mascota")
                            .OrderBy(x => x.FechaAtencion).Where(x => x.Estado == estado && DateTime.Today <= x.FechaAtencion)
                            .ToList();
                    else if(estado == 0)//Anuladas, Expiradas
                        consultas = context.Consulta.Include("Cliente").Include("Mascota")
                            .OrderBy(x => x.FechaAtencion).Where(x => x.Estado == estado || (DateTime.Today > x.FechaAtencion && x.Estado == 1))
                            .ToList();
                    else if(estado == 2)//Atendidas
                        consultas = context.Consulta.Include("Cliente").Include("Mascota")
                            .OrderBy(x => x.FechaAtencion).Where(x => x.Estado == estado)
                            .ToList();
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }

            return consultas;
        }

        public List<Consulta> GetAllConsultasDia()
        {
            List<Consulta> consultas = new List<Consulta>();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    consultas = context.Consulta.Include("Cliente").Include("Mascota")
                        .OrderBy(x => x.FechaAtencion).Where(x => x.Estado == 1 && x.FechaAtencion == DateTime.Today)
                        .ToList();
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }

            return consultas;
        }

        public int CrudConsulta(int id){

            int crud = 0;
            using (var context = new VeterinariaBDContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.ConsultaId == 0)
                        {
                            context.Entry(this).Entity.CajeroId = id;
                            context.Entry(this).Entity.Estado = 1;//Por atender = 1, Anulada = 0, Atendida = 2
                            context.Entry(this).Entity.FechaRegistro = DateTime.Now;
                            context.Entry(this).State = EntityState.Added;
                            crud = 1;
                        }
                        else
                        {
                            context.Configuration.AutoDetectChangesEnabled = false;
                            context.Configuration.ValidateOnSaveEnabled = false;

                            context.Entry(this).State = EntityState.Modified;

                            context.Entry(this).Property(x => x.CajeroId).IsModified = false;
                            context.Entry(this).Property(x => x.Estado).IsModified = false;
                            context.Entry(this).Property(x => x.FechaRegistro).IsModified = false;
                            crud = 2;
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Message = e.Message;

                    }
                }
            }
            return crud;
        }

        public int CambiarEstado(int ConsultaId, int estado)
        {
            int c = 0;

            using (var context = new VeterinariaBDContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        c = context.Database.ExecuteSqlCommand(
                                  "UPDATE Consulta set Estado = "+ estado +" WHERE ConsultaId = @ConsultaId",
                                  new SqlParameter("ConsultaId", ConsultaId)
                              );

                        context.Configuration.ValidateOnSaveEnabled = false;

                        context.SaveChanges();
                        transaction.Commit();

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Message = e.Message;
                    }
                }
            }
            return c;
        }


        public Consulta VerificarConsulta(int ClienteId, int MascotaId, DateTime FechaAtencion)
        {
            Consulta consulta = new Consulta();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    consulta = context.Consulta
                        .Include("Cliente")
                        .Include("Mascota")
                        .Include("Usuario")
                        .Where(x => x.ClienteId == ClienteId && x.MascotaId == MascotaId && x.FechaAtencion.CompareTo(FechaAtencion.Date) == 0)
                        .FirstOrDefault();

                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }
            return consulta;
        }
    }
}
