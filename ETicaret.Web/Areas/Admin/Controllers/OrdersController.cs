using ETicaret.Data;
using ETicaret.Services.Catalog;
using ETicaret.Services.Order;
using ETicaret.Services.System;
using ETicaret.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private IOrderService _orderService;
        private IUrunService _urunService;
        private IPictureService _pictureService;
        public OrdersController(IOrderService orderService, IUrunService urunService, IPictureService pictureService)
        {
            _orderService = orderService;
            _urunService = urunService;
            _pictureService = pictureService;
        }
        // GET: Admin/Orders
        public ActionResult Index()
        {
            var model = _orderService.GetAllOrders().ToList().Select(f => PrepareOrderIndexModel(f));
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = PrepareOrderDetailsModel(_orderService.GetOrderById(id));
            return View(model);
        }

        public void changeStatus(int id, int statusId)
        {
            _orderService.ChangeOrderStatus(id, statusId);
        }

        [NonAction]
        private OrderIndexModel PrepareOrderIndexModel(Siparis siparis)
        {
            var model = new OrderIndexModel();
            model.Id = siparis.Id;
            model.Musteri = siparis.Kullanici.Adi + " " + siparis.Kullanici.Soyadi;
            model.Tarih = siparis.Tarih;
            model.Tutar = siparis.Tutar;
            model.UrunAdedi = siparis.SiparisUrun.Count;
            model.DurumEnum = (OrderStatus)siparis.SiparisDurumu;
            return model;
        }

        [NonAction]
        private OrderDetailsModel PrepareOrderDetailsModel(Siparis siparis)
        {
            var model = new OrderDetailsModel();
            model.DurumEnum = (OrderStatus)siparis.SiparisDurumu;
            model.Id = siparis.Id;
            model.Musteri = siparis.Kullanici.Adi + " " + siparis.Kullanici.Soyadi;
            model.Tarih = siparis.Tarih;
            model.Tutar = siparis.Tutar;
            model.FaturaAdresi = new Web.Models.AddressModel
            {
                Adres = siparis.Adres.Adres1,
                AdSoyad = siparis.Adres.AdSoyad,
                PostaKodu = siparis.Adres.PostaKodu,
                Sehir = siparis.Adres.Sehir,
                SirketAdi = siparis.Adres.Sirket,
                Telefon = siparis.Adres.Telefon,
                Ulke = siparis.Adres.Ulke,
                VergiNo = siparis.Adres.VergiNo
            };
            model.KargoAdresi = new Web.Models.AddressModel
            {
                Adres = siparis.Adres1.Adres1,
                AdSoyad = siparis.Adres1.AdSoyad,
                PostaKodu = siparis.Adres1.PostaKodu,
                Sehir = siparis.Adres1.Sehir,
                SirketAdi = siparis.Adres1.Sirket,
                Telefon = siparis.Adres1.Telefon,
                Ulke = siparis.Adres1.Ulke,
                VergiNo = siparis.Adres1.VergiNo
            };

            model.GercekTutar = siparis.GercekTutari;
            model.Guid = siparis.Guid.ToString();
            foreach (var item in siparis.SiparisUrun)
            {
                var urun = _urunService.GetProductById(item.UrunId);
                model.Urunler.Add(new OrderProductModel
                {
                    Adet = item.Adet,
                    Id = item.UrunId,
                    UrunAdi = urun.UrunAdi,
                    GercekTutar = item.GercekFiyat,
                    Tutar = item.Fiyat,
                    Resim = _pictureService.GetPictureById(urun.UrunResim.FirstOrDefault().ResimId).DosyaYol
                });
            }
            return model;
        }
    }
}