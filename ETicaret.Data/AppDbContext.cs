namespace ETicaret.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=AppDbContext")
        {
        }
        
        public virtual DbSet<Adres> Adres { get; set; }
        public virtual DbSet<KargolamaSure> KargolamaSure { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Kategori_Urun_Mapping> Kategori_Urun_Mapping { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Resim> Resim { get; set; }
        public virtual DbSet<ResimKlasor> ResimKlasor { get; set; }
        public virtual DbSet<Sepet> Sepet { get; set; }
        public virtual DbSet<Siparis> Siparis { get; set; }
        public virtual DbSet<SiparisUrun> SiparisUrun { get; set; }
        public virtual DbSet<Urun> Urun { get; set; }
        public virtual DbSet<UrunResim> UrunResim { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Slideshow> Slideshow { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adres>()
                .HasMany(e => e.Kullanici1)
                .WithOptional(e => e.Adres1)
                .HasForeignKey(e => e.FaturaAdresiId);

            modelBuilder.Entity<Adres>()
                .HasMany(e => e.Kullanici2)
                .WithOptional(e => e.Adres2)
                .HasForeignKey(e => e.KargoAdresiId);

            modelBuilder.Entity<Adres>()
                .HasMany(e => e.Siparis)
                .WithRequired(e => e.Adres)
                .HasForeignKey(e => e.FaturaAdresiId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Adres>()
                .HasMany(e => e.Siparis1)
                .WithRequired(e => e.Adres1)
                .HasForeignKey(e => e.KargoAdresiId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KargolamaSure>()
                .HasMany(e => e.Urun)
                .WithOptional(e => e.KargolamaSure)
                .HasForeignKey(e => e.KargolamaSuresiId);

            modelBuilder.Entity<Kategori>()
                .HasMany(e => e.Kategori_Urun_Mapping)
                .WithRequired(e => e.Kategori)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Adres)
                .WithRequired(e => e.Kullanici)
                .HasForeignKey(e => e.KullaniciId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Kategori)
                .WithRequired(e => e.Kullanici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Resim)
                .WithRequired(e => e.Kullanici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Sepet)
                .WithRequired(e => e.Kullanici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Siparis)
                .WithRequired(e => e.Kullanici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.SildigiUrunler)
                .WithOptional(e => e.SilenKullanici)
                .HasForeignKey(e => e.DeletedKullaniciId);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.OlusturduguUrunler)
                .WithRequired(e => e.OlusturanKullanici)
                .HasForeignKey(e => e.KullaniciId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.GuncelledigiUrunler)
                .WithOptional(e => e.GuncelleyenKullanici)
                .HasForeignKey(e => e.SonGuncelleyenKisi);

            modelBuilder.Entity<ResimKlasor>()
                .HasMany(e => e.Resim)
                .WithOptional(e => e.ResimKlasor)
                .HasForeignKey(e => e.KlasorId);

            modelBuilder.Entity<Siparis>()
                .HasMany(e => e.SiparisUrun)
                .WithRequired(e => e.Siparis)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.Kategori_Urun_Mapping)
                .WithRequired(e => e.Urun)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.Sepet)
                .WithRequired(e => e.Urun)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.UrunResim)
                .WithRequired(e => e.Urun)
                .WillCascadeOnDelete(false);
        }
    }
}
