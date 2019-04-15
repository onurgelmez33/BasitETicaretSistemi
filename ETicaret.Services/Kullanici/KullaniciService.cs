using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Data;
using ETicaret.Services.Mesajlasma;

namespace ETicaret.Services.Kullanici
{
    public class KullaniciService : IKullaniciService
    {
        private AppDbContext db = new AppDbContext();
        private IEmailSenderService _emailSenderService;
        public KullaniciService(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        public void ActivateUserByGuid(string customerGuid)
        {
            var customer = db.Kullanici.FirstOrDefault(f => f.Guid.ToString() == customerGuid);
            customer.EmailOnaylandi = true;
            db.SaveChanges();
        }

        public Data.Kullanici CreateGuestMember(string ipAddress)
        {
            var kullanici = new Data.Kullanici();
            kullanici.Aktif = true;
            kullanici.SonGirisTarihi = DateTime.Now;
            kullanici.EmailOnaylandi = true;
            kullanici.KayitZamani = DateTime.Now;
            kullanici.Guid = Guid.NewGuid();
            kullanici.Email = ipAddress;
            kullanici.SonGirisIp = ipAddress;
            kullanici.Adi = "Guest";
            kullanici.Soyadi = "Guest";
            kullanici.Sifre = Guid.NewGuid().ToString();
            kullanici.Role = "Guest";
            db.Kullanici.Add(kullanici);
            db.SaveChanges();
            return kullanici;
        }

        public bool EmailKontrolEt(string email)
        {
            return db.Kullanici.Any(f => f.Email == email);
        }

        public Data.Kullanici GetUserByEmail(string email)
        {
            return db.Kullanici.SingleOrDefault(f => f.Email == email);
        }

        public Data.Kullanici GetUserByEmailCredentials(string email, string password)
        {
            return db.Kullanici.SingleOrDefault(f => f.Email == email && f.Sifre == password);
        }

        public Data.Kullanici GetUserByUsername(string userName)
        {
            return db.Kullanici.SingleOrDefault(f => f.KullaniciAdi == userName);
        }

        public Data.Kullanici GetUserByUsernameCredentials(string userName, string password)
        {
            return db.Kullanici.SingleOrDefault(f => f.KullaniciAdi == userName && f.Sifre == password);
        }

        public KayitOlSonuc KayitOl(Data.Kullanici kullanici)
        {
            kullanici.Aktif = true;
            kullanici.SonGirisTarihi = DateTime.Now;
            kullanici.EmailOnaylandi = false;
            kullanici.KayitZamani = DateTime.Now;
            kullanici.Guid = Guid.NewGuid();
            db.Kullanici.Add(kullanici);
            db.SaveChanges();
            _emailSenderService.Send(new EmailMessageModel
            {
                Receiver = new List<string> { kullanici.Email },
                Subject = "Üyeliğinizi Aktifleştirin",
                MessageBody = "Üyeliğiniz oluşturulmuştur. <a href=\"http://localhost:2181/Kullanici/ConfirmEmail?CustomerGuid=" + kullanici.Guid.ToString() + "\">Atifleştir</a>"
            });
            return KayitOlSonuc.Basarili;
        }

        public bool KullaniciAdiKontrolEt(string kullaniciAdi)
        {
            return db.Kullanici.Any(f => f.KullaniciAdi == kullaniciAdi);
        }
    }
}
