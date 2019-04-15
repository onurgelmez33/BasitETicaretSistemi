using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Data;

namespace ETicaret.Services.Order
{
    public class OrderService : IOrderService
    {
        private AppDbContext _dbContext;
        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ChangeOrderStatus(int id, int statusId)
        {
            var order = _dbContext.Siparis.SingleOrDefault(f => f.Id == id);
            if (order != null)
            {
                order.SiparisDurumu = statusId;
                _dbContext.SaveChanges();
            }
        }

        public IQueryable<Siparis> GetAllOrders()
        {
            return _dbContext.Siparis;
        }

        public Siparis GetOrderById(int id)
        {
            return _dbContext.Siparis.SingleOrDefault(f => f.Id == id);
        }

        public Siparis PlaceOrder(int userId, IEnumerable<Sepet> sepet, int billingAddressId, int shippingAddressId)
        {
            Siparis siparis = new Siparis();
            siparis.FaturaAdresiId = billingAddressId;
            siparis.Guid = Guid.NewGuid();
            siparis.KargoAdresiId = shippingAddressId;
            siparis.KullaniciId = userId;
            siparis.SiparisDurumu = 10;
            siparis.Tarih = DateTime.Now;
            siparis.Tutar = 0;
            siparis.GercekTutari = 0;
            foreach (var item in sepet)
            {
                SiparisUrun urun = new SiparisUrun();
                urun.Adet = item.Adet;
                urun.UrunId = item.UrunId;
                var fiyat = decimal.Zero;
                if (DateTime.Now < item.Urun.OzelFiyatBitisTarihi && DateTime.Now > item.Urun.OzelFiyatBaslangicTarihi && item.Urun.OzelFiyat.HasValue)
                {
                    fiyat = item.Urun.OzelFiyat.Value;
                }
                else
                {
                    fiyat = item.Urun.Fiyat;
                }
                urun.Fiyat = fiyat * item.Adet;
                urun.Fiyat += item.Urun.KargoDurum ? 0 : item.Urun.KargoFiyat ?? 0;
                urun.GercekFiyat = item.Urun.Fiyat * item.Adet;
                siparis.GercekTutari += urun.GercekFiyat;
                siparis.Tutar += urun.Fiyat;
                siparis.SiparisUrun.Add(urun);
            }
            _dbContext.Siparis.Add(siparis);
            _dbContext.SaveChanges();
            return siparis;
        }
    }
}
