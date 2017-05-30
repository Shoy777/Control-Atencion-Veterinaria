namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;

    [Table("Cliente")]
    public partial class Cliente
    {
        public Cliente()
        {
            Consulta = new HashSet<Consulta>();
            Mascota = new HashSet<Mascota>();
        }

        public int ClienteId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(12)]
        public string Telefono { get; set; }

        public byte Estado { get; set; }

        public virtual ICollection<Consulta> Consulta { get; set; }

        public virtual ICollection<Mascota> Mascota { get; set; }

        [NotMapped]
        public string Message { get; set; }

        [NotMapped]
        public string NombreCompleto { get { return Apellido + " " + Nombre; } }

        public List<Cliente> GetAllClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    clientes = context.Cliente
                        .OrderBy(x => x.Apellido)
                        .ToList();
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }

            return clientes;
        }

        public Cliente GetCliente(int id)
        {
            Cliente cliente = new Cliente();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    cliente = context.Cliente
                        .Include("Mascota")
                        .Where(x => x.ClienteId == id)
                        .Single();
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }
            return cliente;
        }

        public void CrudCliente()
        {
            using (var context = new VeterinariaBDContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.ClienteId == 0)
                        {
                            context.Entry(this).Entity.Estado = 1;//Activo = 1, De Baja = 0
                            context.Entry(this).State = EntityState.Added;
                        }
                        else
                        {
                            context.Entry(this).State = EntityState.Modified;
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        Message = "Registro guardado";

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Message = e.Message;
                    }
                }
            }
        }

        public List<Cliente> GetAllClientesByLast()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    clientes = context.Cliente
                        .OrderByDescending(x => x.ClienteId)
                        .ToList();
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }

            return clientes;
        }
    }
}
