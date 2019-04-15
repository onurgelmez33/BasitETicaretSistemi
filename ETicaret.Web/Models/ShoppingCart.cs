using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Web.Models
{
    public class ShoppingCartModel
    {
        public int UrunId { get; set; }
        public int Adet { get; set; }
        public string UrunAdi { get; set; }
        public decimal Fiyat { get; set; }
        public string Link { get; set; }
        public string ResimYol { get; set; }
    }
}