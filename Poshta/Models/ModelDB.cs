namespace Poshta.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDB : DbContext
    {
        public ModelDB()
            : base("name=ModelDB")
        {
        }

        public virtual DbSet<CASTOMER> CASTOMER { get; set; }
        public virtual DbSet<NAKLADNA> NAKLADNA { get; set; }
        public virtual DbSet<PACKAGE> PACKAGE { get; set; }
        public virtual DbSet<REGIONCollection> REGIONCollection { get; set; }
        public virtual DbSet<ROLES> ROLES { get; set; }
        public virtual DbSet<SECTION> SECTION { get; set; }
        public virtual DbSet<SectionPackage> SectionPackage { get; set; }
        public virtual DbSet<StanP> StanP { get; set; }
        public virtual DbSet<StanS> StanS { get; set; }
        public virtual DbSet<StanTransp> StanTransp { get; set; }
        public virtual DbSet<StanU> StanU { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<USER> USER { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CASTOMER>()
                .HasMany(e => e.NAKLADNA)
                .WithRequired(e => e.CASTOMER)
                .HasForeignKey(e => e.phone_v)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CASTOMER>()
                .HasMany(e => e.NAKLADNA1)
                .WithRequired(e => e.CASTOMER1)
                .HasForeignKey(e => e.phone_g)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PACKAGE>()
                .HasMany(e => e.NAKLADNA)
                .WithRequired(e => e.PACKAGE)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<PACKAGE>()
                .HasMany(e => e.SectionPackage)
                .WithRequired(e => e.PACKAGE)
                .HasForeignKey(e => e.id_section)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<REGIONCollection>()
                .HasMany(e => e.SECTION)
                .WithRequired(e => e.REGIONCollection)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ROLES>()
                .HasMany(e => e.USER)
                .WithRequired(e => e.ROLES)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SECTION>()
                .HasMany(e => e.NAKLADNA)
                .WithRequired(e => e.SECTION)
                .HasForeignKey(e => e.id_section_g)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SECTION>()
                .HasMany(e => e.SectionPackage)
                .WithRequired(e => e.SECTION)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<SECTION>()
                .HasMany(e => e.USER)
                .WithRequired(e => e.SECTION)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<StanP>()
                .HasMany(e => e.PACKAGE)
                .WithRequired(e => e.StanP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StanS>()
                .Property(e => e.stan_s)
                .IsFixedLength();

            modelBuilder.Entity<StanS>()
                .HasMany(e => e.SECTION)
                .WithRequired(e => e.StanS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StanTransp>()
                .HasMany(e => e.NAKLADNA)
                .WithRequired(e => e.StanTransp)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StanU>()
                .HasMany(e => e.USER)
                .WithRequired(e => e.StanU)
                .HasForeignKey(e => e.stan_u)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.NAKLADNA)
                .WithRequired(e => e.USER)
                .HasForeignKey(e => e.contact_number_v)
                .WillCascadeOnDelete(true);
        }

        public System.Data.Entity.DbSet<Poshta.Models.sklad> sklads { get; set; }
    }
}
