namespace Modelos
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class contextParcial : DbContext
    {
        public contextParcial()
            : base("name=contextParcial")
        {
        }

        public virtual DbSet<eventos> eventos { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<eventos>()
                .Property(e => e.evento)
                .IsUnicode(false);

            modelBuilder.Entity<eventos>()
                .Property(e => e.lugar)
                .IsUnicode(false);

            modelBuilder.Entity<eventos>()
                .Property(e => e.costo)
                .HasPrecision(8, 2);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.apellido)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.clave)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.imagen)
                .IsUnicode(false);
        }
    }
}
