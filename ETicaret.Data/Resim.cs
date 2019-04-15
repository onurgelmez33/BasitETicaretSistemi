namespace ETicaret.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Resim")]
    public partial class Resim
    {
        public int Id { get; set; }

        public int? KlasorId { get; set; }

        [Required]
        [StringLength(250)]
        public string DosyaYol { get; set; }

        public int KullaniciId { get; set; }

        public DateTime OlusturulmaTarihi { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual ResimKlasor ResimKlasor { get; set; }
    }
}
