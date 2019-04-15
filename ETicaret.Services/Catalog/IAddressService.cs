using ETicaret.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.Catalog
{
    public interface IAddressService
    {
        IQueryable<Adres> KullanicininAdresleriniGetir(string username);
        Adres AdresEkle(Adres adres);
    }
}
