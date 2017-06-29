namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;
    using System.Text;

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
        [Display(Name="Nombre *")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Apellido *")]
        public string Apellido { get; set; }

        [Required]
        [StringLength(12)]
        [Display(Name = "Telefono *")]
        public string Telefono { get; set; }

        public byte Estado { get; set; }

        public virtual ICollection<Consulta> Consulta { get; set; }

        public virtual ICollection<Mascota> Mascota { get; set; }

        /*
         * Campo Adicional Creado por conflicto de 
         * data entity dynamic proxies
         * al retornar nombre y apellido concatenado
         * public string NombreCompleto { get { return Nombre + " " + Apellido; } }
         */
        public string NombreCompleto { get; set; }

        [NotMapped]
        public string Message { get; set; }

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
                    new Exception(e.Message);
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
                    new Exception(e.Message);
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
                        StringBuilder name = new StringBuilder();
                        name.Append(this.Nombre);
                        name.Append(" ");
                        name.Append(this.Apellido);

                        if (this.ClienteId == 0)
                        {
                            context.Entry(this).Entity.Estado = 1;//Activo = 1, De Baja = 0
                            context.Entry(this).Entity.NombreCompleto = name.ToString();
                            context.Entry(this).State = EntityState.Added;
                        }
                        else
                        {
                            context.Entry(this).Entity.NombreCompleto = name.ToString();
                            context.Entry(this).State = EntityState.Modified;
                        }
                        context.SaveChanges();
                        transaction.Commit();

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        new Exception(e.Message);
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
                        .Include("Mascota")
                        .OrderByDescending(x => x.ClienteId)
                        .ToList();
                }
                catch (Exception e)
                {
                    new Exception(e.Message);
                }
            }

            return clientes;
        }

        public Cliente GetClienteByQuery(string query)
        {
            Cliente cliente = new Cliente();

            using (var context = new VeterinariaBDContext())
            {
                try
                {
                    cliente = context.Cliente
                        .Include(m => m.Mascota)
                        .Where(x => x.Nombre == query)
                        .Single();
                }
                catch (Exception e)
                {
                    new Exception(e.Message);
                }
            }
            return cliente;
        }

    }
}
