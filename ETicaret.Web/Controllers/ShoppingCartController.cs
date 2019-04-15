using ETicaret.Services.Catalog;
using ETicaret.Services.ShoppingCart;
using ETicaret.Services.System;
using ETicaret.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IShoppingCartService _shoppingCartService;
        private IUrunService _urunService;
        private IPictureService _pictureService;
        private string UserId => User.Identity.Name;
        public ShoppingCartController(IShoppingCartService shoppingCartService,
            IUrunService urunService,
            IPictureService pictureService)
        {
            _shoppingCartService = shoppingCartService;
            _urunService = urunService;
            _pictureService = pictureService;
        }
        public ActionResult _SepetiGetir()
        {
            if (User != null && !string.IsNullOrEmpty(UserId))
            {
                var data = _shoppingCartService.SepetiGetir(UserId).ToList().Select(f => PrepareShoppingCartModel(f));
                return PartialView(data);
            }
            else
            {
                return PartialView();
            }
        }

        public ActionResult Index()
        {
            var data = _shoppingCartService.SepetiGetir(UserId).ToList().Select(f => PrepareShoppingCartModel(f));
            return View(data);
        }

        public ActionResult SepeteEkle(int urunId, int adet = 1)
        {
            _shoppingCartService.SepeteEkle(urunId, adet, UserId);
            return RedirectToAction("_SepetiGetir");
        }

        public ActionResult SepettenSil(int urunId)
        {
            _shoppingCartService.SepettenUrunSil(urunId, UserId);
            return RedirectToAction("_SepetiGetir");
        }

        public ActionResult SepetAzalt(int urunId)
        {
            _shoppingCartService.SepetAzalt(urunId, UserId);
            return RedirectToAction("_SepetiGetir");
        }

        [NonAction]
        public ShoppingCartModel PrepareShoppingCartModel(Data.Sepet sepet)
        {
            var model = new ShoppingCartModel();
            var urun = _urunService.GetProductById(sepet.UrunId);
            model.Adet = sepet.Adet;
            model.UrunId = sepet.UrunId;
            var fiyat = decimal.Zero;
            if (DateTime.Now < sepet.Urun.OzelFiyatBitisTarihi && DateTime.Now > sepet.Urun.OzelFiyatBaslangicTarihi && sepet.Urun.OzelFiyat.HasValue)
            {
                fiyat = sepet.Urun.OzelFiyat.Value;
            }
            else
            {
                fiyat = sepet.Urun.Fiyat;
            }
            model.Fiyat = fiyat;
            model.Link = urun.Slug;
            model.UrunAdi = urun.UrunAdi;
            if (urun.UrunResim.Any())
            {
                model.ResimYol = _pictureService.GetPictureById(urun.UrunResim.FirstOrDefault().ResimId).DosyaYol;
            }
            else
            {
                model.ResimYol = _pictureService.GetPictureById(0).DosyaYol;
            }
            return model;
        }
    }
}