using ETicaret.Data;
using ETicaret.Services.Catalog;
using ETicaret.Services.Kullanici;
using ETicaret.Services.Order;
using ETicaret.Services.ShoppingCart;
using ETicaret.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CheckoutController : Controller
    {
        private IAddressService _addressService;
        private IKullaniciService _kullaniciService;
        private IShoppingCartService _shoppingCartService;
        private IOrderService _orderService;
        private int UserId => _kullaniciService.GetUserByEmail(User.Identity.Name).Id;
        public CheckoutController(IAddressService addressService, 
            IKullaniciService kullaniciService,
            IShoppingCartService shoppingCartService,
            IOrderService orderService)
        {
            _addressService = addressService;
            _kullaniciService = kullaniciService;
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
        }
        // GET: Checkout
        public ActionResult BillingPage()
        {
            if (!_shoppingCartService.SepetiGetir(User.Identity.Name).Any())
            {
                return View("CartEmpty");
            }
            var data = _addressService.KullanicininAdresleriniGetir(User.Identity.Name).ToList().Select(f => PrepareAddressModel(f));
            ViewData["Addresses"] = data.OrderByDescending(f => f.Id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BillingPage(BillingAddressModel model)
        {
            if (!_shoppingCartService.SepetiGetir(User.Identity.Name).Any())
            {
                return View("CartEmpty");
            }
            var address = _addressService.AdresEkle(new Adres
            {
                Adres1 = model.Adres,
                AdSoyad = model.AdSoyad,
                KullaniciId = _kullaniciService.GetUserByEmail(User.Identity.Name).Id,
                PostaKodu = model.PostaKodu,
                Sehir = model.Sehir,
                Sirket = model.SirketAdi,
                Telefon = model.Telefon,
                Ulke = model.Ulke,
                VergiNo = model.VergiNo
            });
            return RedirectToAction("ShippingPage", new { billingId = address.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectBillingAddress(int? addressId)
        {
            if (!_shoppingCartService.SepetiGetir(User.Identity.Name).Any())
            {
                return View("CartEmpty");
            }
            if (addressId == null)
            {
                return RedirectToAction("BillingPage");
            }
            return RedirectToAction("ShippingPage", new { billingId = addressId });
        }

        public ActionResult ShippingPage(int? billingId)
        {
            if (!_shoppingCartService.SepetiGetir(User.Identity.Name).Any())
            {
                return View("CartEmpty");
            }
            if (billingId == null)
            {
                return RedirectToAction("BillingPage");
            }
            var data = _addressService.KullanicininAdresleriniGetir(User.Identity.Name).ToList().Select(f => PrepareAddressModel(f));
            ViewData["Addresses"] = data.OrderByDescending(f => f.Id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShippingPage(int? billingId, ShippingAddressModel model)
        {
            if (!_shoppingCartService.SepetiGetir(User.Identity.Name).Any())
            {
                return View("CartEmpty");
            }
            if (billingId == null)
            {
                return RedirectToAction("BillingPage");
            }
            var address = _addressService.AdresEkle(new Adres
            {
                Adres1 = model.Adres,
                AdSoyad = model.AdSoyad,
                KullaniciId = _kullaniciService.GetUserByEmail(User.Identity.Name).Id,
                PostaKodu = model.PostaKodu,
                Sehir = model.Sehir,
                Sirket = model.SirketAdi,
                Telefon = model.Telefon,
                Ulke = model.Ulke,
                VergiNo = model.VergiNo
            });
            return RedirectToAction("PaymentPage", new { shippingId = address.Id, billingId = billingId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectShippingAddress(int? billingId, int? addressId)
        {
            if (!_shoppingCartService.SepetiGetir(User.Identity.Name).Any())
            {
                return View("CartEmpty");
            }
            if (billingId == null || addressId == null)
            {
                return RedirectToAction("BillingPage");
            }
            return RedirectToAction("PaymentPage", new { billingId = billingId, shippingId = addressId });
        }

        public ActionResult PaymentPage(int? billingId, int? shippingId)
        {
            if (!_shoppingCartService.SepetiGetir(User.Identity.Name).Any())
            {
                return View("CartEmpty");
            }
            if (billingId == null || shippingId == null)
            {
                return RedirectToAction("BillingPage");
            }
            CreditCardModel model = new CreditCardModel();
            model.BillingAddressId = billingId ?? 0;
            model.ShippingAddressId = shippingId ?? 0;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentPage(CreditCardModel model)
        {
            if (!_shoppingCartService.SepetiGetir(User.Identity.Name).Any())
            {
                return View("CartEmpty");
            }
            var order = _orderService.PlaceOrder(UserId, _shoppingCartService.SepetiGetir(User.Identity.Name), model.BillingAddressId, model.ShippingAddressId);
            _shoppingCartService.SepetiBosalt(User.Identity.Name);
            return View("OrderCompleted", order.Id);
        }

        [NonAction]
        private AddressModel PrepareAddressModel(Adres adres)
        {
            AddressModel model = new AddressModel();
            model.Id = adres.Id;
            model.AdSoyad = adres.AdSoyad;
            model.Adres = adres.Adres1;
            model.PostaKodu = adres.PostaKodu;
            model.Sehir = adres.Sehir;
            model.SirketAdi = adres.Sirket;
            model.Telefon = adres.Telefon;
            model.Ulke = adres.Ulke;
            model.VergiNo = adres.VergiNo;
            return model;
        }
    }
}