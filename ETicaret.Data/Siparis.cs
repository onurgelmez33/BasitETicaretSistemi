namespace ETicaret.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Siparis
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Siparis()
        {
            SiparisUrun = new HashSet<SiparisUrun>();
        }

        public int Id { get; set; }

        public Guid Guid { get; set; }

        public int KullaniciId { get; set; }

        public DateTime Tarih { get; set; }

        public decimal Tutar { get; set; }

        public decimal GercekTutari { get; set; }

        public int FaturaAdresiId { get; set; }

        public int KargoAdresiId { get; set; }

        public int SiparisDurumu { get; set; }

        public virtual Adres Adres { get; set; }

        public virtual Adres Adres1 { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiparisUrun> SiparisUrun { get; set; }
    }
}
