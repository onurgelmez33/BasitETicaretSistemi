using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Data;

namespace ETicaret.Services.Catalog
{
    public class AddressService : IAddressService
    {
        private AppDbContext _dbContext;
        public AddressService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Adres AdresEkle(Adres adres)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Adres> KullanicininAdresleriniGetir(string username)
        {
            var userId = _dbContext.Kullanici.SingleOrDefault(f => f.Email == username).Id;
            return _dbContext.Adres.Where(f => f.KullaniciId == userId);
        }
    }
}
