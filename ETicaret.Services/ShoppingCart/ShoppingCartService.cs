using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Data;
using ETicaret.Services.Catalog;

namespace ETicaret.Services.ShoppingCart
{
    public class ShoppingCartService : IShoppingCartService
    {
        AppDbContext db = new AppDbContext();
        private IUrunService _urunService;
        public ShoppingCartService(IUrunService urunService)
        {
            _urunService = urunService;
        }
        public void SepeteEkle(int urunId, int quantity, string username)
        {
            var userId = db.Kullanici.SingleOrDefault(f => f.Email == username).Id;
            if (_urunService.GetProductById(urunId) != null)
            {
                if (db.Sepet.Any(f => f.KullaniciId == userId && f.UrunId == urunId))
                {
                    var sepet = db.Sepet.SingleOrDefault(f => f.KullaniciId == userId && f.UrunId == urunId);
                    sepet.Adet++;
                }
                else
                {
                    db.Sepet.Add(new Sepet
                    {
                        UrunId = urunId,
                        Adet = quantity,
                        KullaniciId = userId,
                        Tutar = 0
                    });
                }
                db.SaveChanges();
            }
        }
        public void SepetAzalt(int urunId, string username)
        {
            var userId = db.Kullanici.SingleOrDefault(f => f.Email == username).Id;
            if (_urunService.GetProductById(urunId) != null)
            {
                if (db.Sepet.Any(f => f.KullaniciId == userId && f.UrunId == urunId))
                {
                    var sepet = db.Sepet.SingleOrDefault(f => f.KullaniciId == userId && f.UrunId == urunId);
                    if (sepet.Adet > 1)
                    {
                        sepet.Adet--;
                    }
                    else
                    {
                        db.Sepet.Remove(sepet);
                    }
                }
                db.SaveChanges();
            }
        }

        public void SepetGuncelle(IEnumerable<Sepet> sepet, string username)
        {
            var userId = db.Kullanici.SingleOrDefault(f => f.Email == username).Id;
            if (SepetiGetir(username).Count() > 0)
            {
                var kullaniciSepet = db.Sepet.Where(f => f.KullaniciId == userId);
                db.Sepet.RemoveRange(kullaniciSepet);
                db.SaveChanges();
                foreach (var item in sepet)
                {
                    SepeteEkle(item.UrunId, item.Adet, username);
                }
            }
        }

        public IQueryable<Sepet> SepetiGetir(string username)
        {
            var userId = db.Kullanici.SingleOrDefault(f => f.Email == username).Id;
            return db.Sepet.Where(f => f.KullaniciId == userId);
        }

        public void SepettenUrunSil(int urunId, string username)
        {
            var userId = db.Kullanici.SingleOrDefault(f => f.Email == username).Id;
            var urun = db.Sepet.SingleOrDefault(f => f.UrunId == urunId && f.KullaniciId == userId);
            if (urun != null)
            {
                db.Sepet.Remove(urun);
                db.SaveChanges();
            }
        }

        public void SepetiBosalt(string username)
        {
            var userId = db.Kullanici.SingleOrDefault(f => f.Email == username).Id;
            db.Sepet.RemoveRange(db.Sepet.Where(f => f.KullaniciId == userId));
            db.SaveChanges();
        }
    }
}
