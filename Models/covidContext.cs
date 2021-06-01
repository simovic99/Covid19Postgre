using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication5
{
    public partial class covidContext : DbContext
    {
        public covidContext()
        {
        }

        public covidContext(DbContextOptions<covidContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Gradovi> Gradovis { get; set; }
        public virtual DbSet<Nalazi> Nalazis { get; set; }
        public virtual DbSet<Pacijenti> Pacijentis { get; set; }
      
        public virtual DbSet<Testovi> Testovis { get; set; }
        public virtual DbSet<Uplate> Uplates { get; set; }
        public virtual DbSet<Ustanove> Ustanoves { get; set; }
        public virtual DbSet<Zupanije> Zupanijes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=covid;Username=admin;Password=proboj10");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Croatian_Croatia.1252");

            modelBuilder.Entity<Gradovi>(entity =>
            {
                entity.ToTable("gradovi");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasIdentityOptions(5L, null, null, null, null, null);

                entity.Property(e => e.BrojStanovnika).HasColumnName("broj_stanovnika");

                entity.Property(e => e.FkZupanija).HasColumnName("fk_zupanija");

                entity.Property(e => e.Naziv)
                    .HasColumnType("character varying")
                    .HasColumnName("naziv");

                entity.HasOne(d => d.FkZupanijaNavigation)
                    .WithMany(p => p.Gradovis)
                    .HasForeignKey(d => d.FkZupanija)
                    .HasConstraintName("gradovi_fk_zupanija_fkey");
            });

            modelBuilder.Entity<Nalazi>(entity =>
            {
                entity.ToTable("nalazi");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(5L, null, null, null, null, null);

                entity.Property(e => e.Datum)
                    .HasColumnType("date")
                    .HasColumnName("datum");

                entity.Property(e => e.PacijentFk).HasColumnName("pacijent_fk");

                entity.Property(e => e.Rezultat)
                    .HasColumnType("character varying")
                    .HasColumnName("rezultat");

                entity.Property(e => e.TestFk).HasColumnName("test_fk");

                entity.Property(e => e.UplataFk).HasColumnName("uplata_fk");

                entity.Property(e => e.UstanovaFk).HasColumnName("ustanova_fk");

                entity.HasOne(d => d.PacijentFkNavigation)
                    .WithMany(p => p.Nalazis)
                    .HasForeignKey(d => d.PacijentFk)
                    .HasConstraintName("nalazi_pacijent_fk_fkey");

                entity.HasOne(d => d.TestFkNavigation)
                    .WithMany(p => p.Nalazis)
                    .HasForeignKey(d => d.TestFk)
                    .HasConstraintName("nalazi_test_fk_fkey");

                entity.HasOne(d => d.UplataFkNavigation)
                    .WithMany(p => p.Nalazis)
                    .HasForeignKey(d => d.UplataFk)
                    .HasConstraintName("nalazi_uplata_fk_fkey");

                entity.HasOne(d => d.UstanovaFkNavigation)
                    .WithMany(p => p.Nalazis)
                    .HasForeignKey(d => d.UstanovaFk)
                    .HasConstraintName("nalazi_ustanova_fk_fkey");
            });

            modelBuilder.Entity<Pacijenti>(entity =>
            {
                entity.ToTable("pacijenti");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(4L, null, null, null, null, null);

                entity.Property(e => e.DatumRodjenja)
                    .HasColumnType("date")
                    .HasColumnName("datum_rodjenja");

                entity.Property(e => e.Email)
                    .HasColumnType("character varying")
                    .HasColumnName("email");

                entity.Property(e => e.GradFk).HasColumnName("grad_fk");

                entity.Property(e => e.Ime)
                    .HasColumnType("character varying")
                    .HasColumnName("ime");

                entity.Property(e => e.Prezime)
                    .HasColumnType("character varying")
                    .HasColumnName("prezime");

                entity.Property(e => e.Telefon)
                    .HasColumnType("character varying")
                    .HasColumnName("telefon");

                entity.HasOne(d => d.GradFkNavigation)
                    .WithMany(p => p.Pacijentis)
                    .HasForeignKey(d => d.GradFk)
                    .HasConstraintName("pacijenti_grad_fk_fkey");
            });

         

            modelBuilder.Entity<Testovi>(entity =>
            {
                entity.ToTable("testovi");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(5L, null, null, null, null, null);

                entity.Property(e => e.Cijena).HasColumnName("cijena");

                entity.Property(e => e.Naziv)
                    .HasColumnType("character varying")
                    .HasColumnName("naziv");
            });

            modelBuilder.Entity<Uplate>(entity =>
            {
                entity.ToTable("uplate");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(5L, null, null, null, null, null);

                entity.Property(e => e.Iznos).HasColumnName("iznos");

                entity.Property(e => e.Vrsta)
                    .HasColumnType("character varying")
                    .HasColumnName("vrsta");
            });

            modelBuilder.Entity<Ustanove>(entity =>
            {
                entity.ToTable("ustanove");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(5L, null, null, null, null, null);

                entity.Property(e => e.GradFk).HasColumnName("grad_fk");

                entity.Property(e => e.Naziv)
                    .HasColumnType("character varying")
                    .HasColumnName("naziv");

                entity.HasOne(d => d.GradFkNavigation)
                    .WithMany(p => p.Ustanoves)
                    .HasForeignKey(d => d.GradFk)
                    .HasConstraintName("ustanove_grad_fk_fkey");
            });

            modelBuilder.Entity<Zupanije>(entity =>
            {
                entity.ToTable("zupanije");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(5L, null, null, null, null, null);

                entity.Property(e => e.BrojStanovnika).HasColumnName("broj_stanovnika");

                entity.Property(e => e.Naziv)
                    .HasColumnType("character varying")
                    .HasColumnName("naziv");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
