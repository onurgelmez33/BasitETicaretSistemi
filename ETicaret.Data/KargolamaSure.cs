namespace ETicaret.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KargolamaSure")]
    public partial class KargolamaSure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KargolamaSure()
        {
            Urun = new HashSet<Urun>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string KargolamaSuresi { get; set; }

        [Required]
        [StringLength(50)]
        public string RenkKodu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urun> Urun { get; set; }
    }
}
