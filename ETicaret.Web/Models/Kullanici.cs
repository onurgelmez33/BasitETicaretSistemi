using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaret.Web.Models
{
    public class KullaniciKayitModel
    {
        [Required(ErrorMessage = "Bu Email Alanını boş bırakamazsınız")]
        public string Email { get; set; }
        [Required]
        public string Ad { get; set; }
        [Required]
        public string Soyadi { get; set; }
        public string KullaniciAdi { get; set; }
        [Required]
        public string Sifre { get; set; }
        [Required]
        public string SifreTekrar { get; set; }
        public string Telefon { get; set; }
        public bool KullaniciAdiEtkin { get; set; }
    }

    public class CustomerLoginModel
    {
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu Alanı boş bırakamazsınız")]
        public string Password { get; set; }
        public string KullaniciAdi { get; set; }
        public bool RememberMe { get; set; }
        public bool KullaniciAdiEtkin { get; set; }
    }
}