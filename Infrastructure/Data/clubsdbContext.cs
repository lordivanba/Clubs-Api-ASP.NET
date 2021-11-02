using System;
using clubs_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace clubs_api.Infrastructure.Data
{
    public partial class clubsdbContext : DbContext
    {
        public clubsdbContext()
        {
        }

        public clubsdbContext(DbContextOptions<clubsdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Club> Clubs { get; set; }
        public virtual DbSet<ServicioClub> ServicioClubs { get; set; }
        public virtual DbSet<Torneo> Torneos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-SHTCNF0\\SQLEXPRESS;Initial Catalog=clubsdb;User ID=sa;Password=12345;Persist Security Info=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Club>(entity =>
            {
                entity.ToTable("Club");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(75)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ServicioClub>(entity =>
            {
                entity.ToTable("ServicioClub");

                entity.Property(e => e.Disciplina)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro).HasColumnType("date");

                entity.Property(e => e.Horario)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.ServicioClubs)
                    .HasForeignKey(d => d.ClubId)
                    .HasConstraintName("FK__ServicioC__ClubI__4BAC3F29");
            });

            modelBuilder.Entity<Torneo>(entity =>
            {
                entity.ToTable("Torneo");

                entity.Property(e => e.Bases)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Disciplina)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro).HasColumnType("date");

                entity.Property(e => e.Resultado)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.TipoTorneo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
