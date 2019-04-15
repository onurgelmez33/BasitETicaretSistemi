namespace ETicaret.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kategori")]
    public partial class Kategori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kategori()
        {
            Kategori_Urun_Mapping = new HashSet<Kategori_Urun_Mapping>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Adi { get; set; }

        [Required]
        [StringLength(250)]
        public string KisaAciklama { get; set; }

        [Required]
        [StringLength(250)]
        public string Aciklama { get; set; }

        public int? UstKategoriId { get; set; }

        public int? ResimId { get; set; }

        public DateTime OlusturulmaTarihi { get; set; }

        public int KullaniciId { get; set; }

        [Required]
        [StringLength(250)]
        public string Slug { get; set; }

        public bool ShowOnHomePage { get; set; }

        public int Sira { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kategori_Urun_Mapping> Kategori_Urun_Mapping { get; set; }
    }
}
