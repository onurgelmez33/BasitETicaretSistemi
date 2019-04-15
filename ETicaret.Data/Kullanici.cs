namespace ETicaret.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kullanici")]
    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
            Adres = new HashSet<Adres>();
            Kategori = new HashSet<Kategori>();
            Resim = new HashSet<Resim>();
            Sepet = new HashSet<Sepet>();
            Siparis = new HashSet<Siparis>();
            SildigiUrunler = new HashSet<Urun>();
            GuncelledigiUrunler = new HashSet<Urun>();
            OlusturduguUrunler = new HashSet<Urun>();
        }

        public int Id { get; set; }

        public Guid Guid { get; set; }

        [Required]
        [StringLength(250)]
        public string Adi { get; set; }

        [Required]
        [StringLength(250)]
        public string Soyadi { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string KullaniciAdi { get; set; }

        [Required]
        [StringLength(250)]
        public string Sifre { get; set; }

        public string Telefon {get; set;}

        public int? KargoAdresiId { get; set; }

        public int? FaturaAdresiId { get; set; }

        public bool EmailOnaylandi { get; set; }

        public bool Aktif { get; set; }

        public DateTime SonGirisTarihi { get; set; }

        [StringLength(50)]
        public string SonGirisIp { get; set; }

        public DateTime KayitZamani { get; set; }

        public string Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adres> Adres { get; set; }

        public virtual Adres Adres1 { get; set; }

        public virtual Adres Adres2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kategori> Kategori { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resim> Resim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sepet> Sepet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparis> Siparis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urun> SildigiUrunler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urun> OlusturduguUrunler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urun> GuncelledigiUrunler { get; set; }
    }
}
