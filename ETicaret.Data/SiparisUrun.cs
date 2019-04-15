namespace ETicaret.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SiparisUrun")]
    public partial class SiparisUrun
    {
        public int Id { get; set; }

        public int SiparisId { get; set; }

        public int UrunId { get; set; }

        public int Adet { get; set; }

        public decimal Fiyat { get; set; }

        public decimal GercekFiyat { get; set; }

        public virtual Siparis Siparis { get; set; }
    }
}
