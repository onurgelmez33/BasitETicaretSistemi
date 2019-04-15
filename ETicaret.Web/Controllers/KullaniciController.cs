using ETicaret.Data;
using ETicaret.Services.Kullanici;
using ETicaret.Services.System;
using ETicaret.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ETicaret.Web.Controllers
{
    public class KullaniciController : Controller
    {
        private IKullaniciService _kullaniciService;
        private ISettingService _settingService;
        public KullaniciController(IKullaniciService kullaniciService,
            ISettingService settingService)
        {
            _kullaniciService = kullaniciService;
            _settingService = settingService;
        }
        // GET: Kullanici
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult KayitOl()
        {
            KullaniciKayitModel model = new KullaniciKayitModel();
            model.KullaniciAdiEtkin = _settingService.GetSetting<bool>("UsernameActivated");
            return View(model);
        }
        [HttpPost]
        public ActionResult KayitOl(KullaniciKayitModel model)
        {
            if (_kullaniciService.EmailKontrolEt(model.Email))
            {
                ModelState.AddModelError("Email", "Bu email zaten bulunmakta.");
                return View(model);
            }
            if (!model.Sifre.Equals(model.SifreTekrar))
            {
                ModelState.AddModelError("Sifre", "Şifre ile şifre tekrar alanlarınız eşleşmiyor.");
                return View(model);
            }
            if (model.KullaniciAdiEtkin)
            {
                if (_kullaniciService.KullaniciAdiKontrolEt(model.KullaniciAdi))
                {
                    ModelState.AddModelError("KullaniciAdi", "Bu kullanıcı adı zaten bulunmakta.");
                    return View(model);
                }
            }
            var kullanici = new Kullanici();
            kullanici.Adi = model.Ad;
            kullanici.Email = model.Email;
            kullanici.KullaniciAdi = model.KullaniciAdi;
            kullanici.Sifre = model.Sifre;
            kullanici.Soyadi = model.Soyadi;
            kullanici.Telefon = model.Telefon;
            var registerResult = _kullaniciService.KayitOl(kullanici);
            if (registerResult == KayitOlSonuc.Basarili)
            {
                return View("ConfirmEmail");
            }
            else
            {
                ModelState.AddModelError("", "Lütfen sistem yetkilisine başvurunuz.");
                return View(model);
            }
        }

        public ActionResult ConfirmEmail(string CustomerGuid)
        {
            try
            {
                _kullaniciService.ActivateUserByGuid(CustomerGuid);
                return View("EmailConfirmed");
            }
            catch (Exception)
            {
                return new HttpNotFoundResult();
            }
        }

        public ActionResult Login()
        {
            //if (!_settingService.GetSetting<bool>("LoginEnabled"))
            //{
            //    return new HttpNotFoundResult();
            //}
            CustomerLoginModel model = new CustomerLoginModel();
            model.KullaniciAdiEtkin = _settingService.GetSetting<bool>("UsernameActivated");
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(CustomerLoginModel model, string returnUrl)
        {
            //if (!_settingService.GetSetting<bool>("LoginEnabled"))
            //{
            //    return new HttpNotFoundResult();
            //}
            if (ModelState.IsValid)
            {
                var customer = new Kullanici();
                if (model.KullaniciAdiEtkin)
                {
                    customer = _kullaniciService.GetUserByUsernameCredentials(model.KullaniciAdi, model.Password);
                }
                else
                {
                    customer = _kullaniciService.GetUserByEmailCredentials(model.Email, model.Password);
                }

                if (customer != null)
                {
                    FormsAuthentication.SetAuthCookie(customer.Email, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Yanlış");
                    return View(model);
                }
            }

            //Buraya kadar geldiyse hata var
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}