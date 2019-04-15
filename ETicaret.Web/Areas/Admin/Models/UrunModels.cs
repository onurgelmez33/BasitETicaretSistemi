using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Web.Areas.Admin.Models
{
    public class UrunIndexModel
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Resim { get; set; }
        public int SatilmaSayisi { get; set; }
        public decimal Fiyat { get; set; }
    }
    public class UrunCreateEditModel
    {
        public int Id { get; set; }
        public string UrunAdi { get; set; }
        public decimal Fiyat { get; set; }
        public HttpPostedFileBase AnaResim { get; set; }
        public IEnumerable<HttpPostedFileBase> Galeri { get; set; }
        public bool Visibility { get; set; }
        public bool ShowOnHomePage { get; set; }
        public string KisaAciklama { get; set; }
        public string Aciklama { get; set; }
        public bool StoklardanDussun { get; set; }
        public int? StokAdeti { get; set; }
        public decimal? Maliyet { get; set; }
        public decimal? EskiFiyat { get; set; }
        public decimal? OzelFiyat { get; set; }
        public DateTime? OzelFiyatBaslangicTarihi { get; set; }
        public DateTime? OzelFiyatBitisTarihi { get; set; }
        public int? KargolamaSuresiId { get; set; }
        public string StokKodu { get; set; }
        public string AdminYorumu { get; set; }
        public int Sira { get; set; }
        public bool KargoDurum { get; set; }
        public decimal? KargoFiyat { get; set; }
        public int[] Categories { get; set; }
        public IEnumerable<string> YukluResimler { get; set; }
    }
}