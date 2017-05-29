namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class VeterinariaBDContext : DbContext
    {
        public VeterinariaBDContext()
            : base("name=VeterinariaBDContext")
        {
        }

        public virtual DbSet<Boleta> Boleta { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Consulta> Consulta { get; set; }
        public virtual DbSet<DetalleBoleta> DetalleBoleta { get; set; }
        public virtual DbSet<Diagnostico> Diagnostico { get; set; }
        public virtual DbSet<Especie> Especie { get; set; }
        public virtual DbSet<Laboratorio> Laboratorio { get; set; }
        public virtual DbSet<Mascota> Mascota { get; set; }
        public virtual DbSet<Medicamento> Medicamento { get; set; }
        public virtual DbSet<Raza> Raza { get; set; }
        public virtual DbSet<Receta> Receta { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<TipoMedicamento> TipoMedicamento { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boleta>()
                .Property(e => e.MontoTotal)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Boleta>()
                .HasMany(e => e.DetalleBoleta)
                .WithRequired(e => e.Boleta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Consulta)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Mascota)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Consulta>()
                .Property(e => e.Precio)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Consulta>()
                .HasMany(e => e.Diagnostico)
                .WithRequired(e => e.Consulta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DetalleBoleta>()
                .Property(e => e.Precio)
                .HasPrecision(6, 2);

            modelBuilder.Entity<DetalleBoleta>()
                .Property(e => e.SubTotal)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Diagnostico>()
                .Property(e => e.DiagnosticoDes)
                .IsUnicode(false);

            modelBuilder.Entity<Diagnostico>()
                .HasMany(e => e.Receta)
                .WithRequired(e => e.Diagnostico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Especie>()
                .HasMany(e => e.Mascota)
                .WithRequired(e => e.Especie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Especie>()
                .HasMany(e => e.Medicamento)
                .WithRequired(e => e.Especie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Especie>()
                .HasMany(e => e.Raza)
                .WithRequired(e => e.Especie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Laboratorio>()
                .HasMany(e => e.Medicamento)
                .WithRequired(e => e.Laboratorio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mascota>()
                .HasMany(e => e.Consulta)
                .WithRequired(e => e.Mascota)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medicamento>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Medicamento>()
                .Property(e => e.Precio)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Medicamento>()
                .HasMany(e => e.DetalleBoleta)
                .WithRequired(e => e.Medicamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medicamento>()
                .HasMany(e => e.Receta)
                .WithRequired(e => e.Medicamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Raza>()
                .HasMany(e => e.Mascota)
                .WithRequired(e => e.Raza)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rol>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoMedicamento>()
                .HasMany(e => e.Medicamento)
                .WithRequired(e => e.TipoMedicamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.DNI)
                .IsFixedLength();

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Consulta)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.CajeroId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Diagnostico)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.VeterinarioId)
                .WillCascadeOnDelete(false);
        }
    }
}
