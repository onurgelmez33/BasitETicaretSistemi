using ETicaret.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.Kullanici
{
    public interface IKullaniciService
    {
        bool EmailKontrolEt(string email);
        bool KullaniciAdiKontrolEt(string kullaniciAdi);
        KayitOlSonuc KayitOl(ETicaret.Data.Kullanici kullanici);
        void ActivateUserByGuid(string customerGuid);
        Data.Kullanici GetUserByUsernameCredentials(string userName, string password);
        Data.Kullanici GetUserByEmailCredentials(string email, string password);
        Data.Kullanici GetUserByUsername(string userName);
        Data.Kullanici GetUserByEmail(string email);
        Data.Kullanici CreateGuestMember(string ipAddress);
    }
}
