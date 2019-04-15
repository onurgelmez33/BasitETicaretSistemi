using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaret.Web.Areas.Admin.Models
{
    public class KategoriIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }
        public string ResimYol { get; set; }
        [Display(Name = "Sıra")]
        public int Sira { get; set; }
        [Display(Name = "Ürün Sayısı")]
        public int UrunSayisi { get; set; }
    }
    public class KategoriEditCreateModel
    {
        public int Id { get; set; }

        public string Adi { get; set; }

        public string KisaAciklama { get; set; }

        public string Aciklama { get; set; }

        public int? UstKategoriId { get; set; }

        public HttpPostedFileBase Resim { get; set; }

        public string ResimUrl { get; set; }

        public bool ShowOnHomePage { get; set; }

        public int Sira { get; set; }
    }
}