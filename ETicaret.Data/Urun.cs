namespace ETicaret.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Urun")]
    public partial class Urun
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urun()
        {
            Kategori_Urun_Mapping = new HashSet<Kategori_Urun_Mapping>();
            Sepet = new HashSet<Sepet>();
            UrunResim = new HashSet<UrunResim>();
        }

        public int Id { get; set; }

        public bool Visibility { get; set; }

        public bool ShowOnHomePage { get; set; }

        [Required]
        [StringLength(250)]
        public string UrunAdi { get; set; }

        [Required]
        [StringLength(250)]
        public string KisaAciklama { get; set; }

        [Required]
        public string Aciklama { get; set; }

        public bool StoklardanDussun { get; set; }

        public int? StokAdeti { get; set; }

        [Required]
        [StringLength(250)]
        public string Slug { get; set; }

        public decimal? Maliyet { get; set; }

        public decimal Fiyat { get; set; }

        public decimal? EskiFiyat { get; set; }

        public decimal? OzelFiyat { get; set; }

        public DateTime? OzelFiyatBaslangicTarihi { get; set; }

        public DateTime? OzelFiyatBitisTarihi { get; set; }

        public int? KargolamaSuresiId { get; set; }

        [StringLength(250)]
        public string StokKodu { get; set; }

        [StringLength(250)]
        public string AdminYorumu { get; set; }

        public int KullaniciId { get; set; }

        public bool? Deleted { get; set; }

        public int? DeletedKullaniciId { get; set; }

        public int? SonGuncelleyenKisi { get; set; }

        public DateTime? GuncellemeTarihi { get; set; }

        public DateTime OlusturulmaTarihi { get; set; }

        public int Sira { get; set; }

        public int Viewed { get; set; }

        public bool KargoDurum { get; set; }

        public decimal? KargoFiyat { get; set; }

        public virtual KargolamaSure KargolamaSure { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kategori_Urun_Mapping> Kategori_Urun_Mapping { get; set; }

        public virtual Kullanici SilenKullanici { get; set; }

        public virtual Kullanici OlusturanKullanici { get; set; }

        public virtual Kullanici GuncelleyenKullanici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sepet> Sepet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrunResim> UrunResim { get; set; }
    }
}
