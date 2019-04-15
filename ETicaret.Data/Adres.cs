namespace ETicaret.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adres
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adres()
        {
            Kullanici1 = new HashSet<Kullanici>();
            Kullanici2 = new HashSet<Kullanici>();
            Siparis = new HashSet<Siparis>();
            Siparis1 = new HashSet<Siparis>();
        }

        public int Id { get; set; }

        public int KullaniciId { get; set; }

        public string AdSoyad { get; set; }

        [Column("Adres")]
        [Required]
        [StringLength(250)]
        public string Adres1 { get; set; }

        [Required]
        [StringLength(250)]
        public string Sehir { get; set; }

        [Required]
        [StringLength(250)]
        public string Ulke { get; set; }

        [StringLength(50)]
        public string PostaKodu { get; set; }

        [StringLength(250)]
        public string Sirket { get; set; }

        [StringLength(50)]
        public string VergiNo { get; set; }

        [StringLength(50)]
        public string Telefon { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kullanici> Kullanici1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kullanici> Kullanici2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparis> Siparis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparis> Siparis1 { get; set; }
    }
}
