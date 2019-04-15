using ETicaret.Data;
using ETicaret.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Web.Areas.Admin.Models
{
    public class OrderIndexModel
    {
        public int Id { get; set; }
        public decimal Tutar { get; set; }
        public DateTime Tarih { get; set; }
        public string Durum { get; set; }
        public string Musteri { get; set; }
        public int UrunAdedi { get; set; }
        public OrderStatus DurumEnum { get; set; }
    }

    public class OrderDetailsModel
    {
        public OrderDetailsModel()
        {
            Urunler = new List<OrderProductModel>();
        }
        public int Id { get; set; }
        public string Guid { get; set; }
        public decimal Tutar { get; set; }
        public decimal GercekTutar { get; set; }
        public DateTime Tarih { get; set; }
        public string Durum { get; set; }
        public string Musteri { get; set; }
        public List<OrderProductModel> Urunler { get; set; }
        public OrderStatus DurumEnum { get; set; }
        public AddressModel FaturaAdresi { get; set; }
        public AddressModel KargoAdresi { get; set; }
    }

    public class OrderProductModel
    {
        public int Id { get; set; }
        public string UrunAdi { get; set; }
        public string Resim { get; set; }
        public int Adet { get; set; }
        public decimal Tutar { get; set; }
        public decimal GercekTutar { get; set; }
    }
}