using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Web.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Ulke { get; set; }
        public string PostaKodu { get; set; }
        public string SirketAdi { get; set; }
        public string VergiNo { get; set; }
        public string Telefon { get; set; }
    }
    public class BillingAddressModel: AddressModel
    {
    }

    public class ShippingAddressModel : AddressModel
    {
    }

    public class CreditCardModel
    {
        public int BillingAddressId { get; set; }
        public int ShippingAddressId { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string expiry { get; set; }
        public int cvc { get; set; }
    }
}