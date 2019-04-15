using ETicaret.Data;
using ETicaret.Services.Catalog;
using ETicaret.Services.Kullanici;
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
    public class ProductsController : Controller
    {
        private IUrunService _urunService;
        private IPictureService _pictureService;
        private IOrderService _orderService;
        private IKategoriService _kategoriService;
        private AppDbContext _dbContext;
        private IKullaniciService _kullaniciService;
        private int userId => _kullaniciService.GetUserByEmail(User.Identity.Name).Id;
        public ProductsController(IUrunService urunService, 
            IPictureService pictureService, 
            IOrderService orderService,
            IKategoriService kategoriService,
            AppDbContext dbContext,
            IKullaniciService kullaniciService)
        {
            _urunService = urunService;
            _pictureService = pictureService;
            _orderService = orderService;
            _kategoriService = kategoriService;
            _dbContext = dbContext;
            _kullaniciService = kullaniciService;
        }
        // GET: Admin/Products
        public ActionResult Index()
        {
            var data = _urunService.GetAllProducts().ToList().Select(f => PrepareUrunIndexModel(f));
            return View(data);
        }

        public void Sil(int id)
        {
            _urunService.DeleteProduct(id);
        }

        public ActionResult Olustur()
        {
            ViewBag.Categories = new SelectList(_kategoriService.GetAll(), "Id", "Adi");
            ViewBag.KargolamaSuresiId = new SelectList(_dbContext.KargolamaSure, "Id", "KargolamaSuresi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Olustur(UrunCreateEditModel model)
        {
            if (ModelState.IsValid)
            {
                _urunService.InsertProduct(new Urun {
                    Aciklama = model.Aciklama,
                    AdminYorumu = model.AdminYorumu,
                    EskiFiyat = model.EskiFiyat,
                    Fiyat = model.Fiyat,
                    KargolamaSuresiId = model.KargolamaSuresiId,
                    KisaAciklama = model.KisaAciklama,
                    KullaniciId = userId,
                    Maliyet = model.Maliyet,
                    OlusturulmaTarihi = DateTime.Now,
                    OzelFiyat = model.OzelFiyat,
                    OzelFiyatBaslangicTarihi = model.OzelFiyatBaslangicTarihi,
                    OzelFiyatBitisTarihi = model.OzelFiyatBitisTarihi,
                    ShowOnHomePage = model.ShowOnHomePage,
                    Sira = model.Sira,
                    StokAdeti = model.StokAdeti,
                    StokKodu = model.StokKodu,
                    StoklardanDussun = model.StoklardanDussun,
                    UrunAdi = model.UrunAdi,
                    Visibility = model.Visibility,
                    KargoDurum = model.KargoDurum,
                    KargoFiyat = model.KargoFiyat
                }, 
                    model.AnaResim, model.Galeri, Server.MapPath("~/uploads"), model.Categories);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(_kategoriService.GetAll(), "Id", "Adi", model.Categories);
            ViewBag.KargolamaSuresiId = new SelectList(_dbContext.KargolamaSure, "Id", "KargolamaSuresi", model.KargolamaSuresiId);
            return View(model);
        }

        public ActionResult Duzenle(int id)
        {
            var data = PrepareUrunCreateEditModel(_urunService.GetProductById(id));
            ViewBag.Categories = new SelectList(_kategoriService.GetAll(), "Id", "Adi", data.Categories);
            ViewBag.KargolamaSuresiId = new SelectList(_dbContext.KargolamaSure, "Id", "KargolamaSuresi", data.KargolamaSuresiId);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(UrunCreateEditModel model)
        {

            if (ModelState.IsValid)
            {
                _urunService.UpdateProduct(new Urun
                {
                    Aciklama = model.Aciklama,
                    AdminYorumu = model.AdminYorumu,
                    EskiFiyat = model.EskiFiyat,
                    Fiyat = model.Fiyat,
                    KargolamaSuresiId = model.KargolamaSuresiId,
                    KisaAciklama = model.KisaAciklama,
                    KullaniciId = userId,
                    Maliyet = model.Maliyet,
                    OlusturulmaTarihi = DateTime.Now,
                    OzelFiyat = model.OzelFiyat,
                    OzelFiyatBaslangicTarihi = model.OzelFiyatBaslangicTarihi,
                    OzelFiyatBitisTarihi = model.OzelFiyatBitisTarihi,
                    ShowOnHomePage = model.ShowOnHomePage,
                    Sira = model.Sira,
                    StokAdeti = model.StokAdeti,
                    StokKodu = model.StokKodu,
                    StoklardanDussun = model.StoklardanDussun,
                    UrunAdi = model.UrunAdi,
                    Visibility = model.Visibility,
                    Id = model.Id,
                    KargoDurum = model.KargoDurum,
                    KargoFiyat = model.KargoFiyat
                },
                    model.AnaResim, model.Galeri, Server.MapPath("~/uploads"), model.Categories);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(_kategoriService.GetAll(), "Id", "Adi", model.Categories);
            ViewBag.KargolamaSuresiId = new SelectList(_dbContext.KargolamaSure, "Id", "KargolamaSuresi", model.KargolamaSuresiId);
            return View(model);
        }

        [NonAction]
        private UrunIndexModel PrepareUrunIndexModel(Urun urun)
        {
            var model = new UrunIndexModel();
            model.Adi = urun.UrunAdi;
            model.Fiyat = urun.Fiyat;
            model.Id = urun.Id;
            model.Resim = _pictureService.GetPictureById(urun.UrunResim.Any() ? urun.UrunResim.FirstOrDefault().ResimId : 0).DosyaYol;
            model.SatilmaSayisi = _orderService.GetAllOrders().Count(f => f.SiparisUrun.Any(k => k.UrunId == urun.Id));
            return model;
        }

        [NonAction]
        private UrunCreateEditModel PrepareUrunCreateEditModel(Urun urun)
        {
            var model = new UrunCreateEditModel();
            model.Aciklama = urun.Aciklama;
            model.AdminYorumu = urun.AdminYorumu;
            model.EskiFiyat = urun.EskiFiyat;
            model.Fiyat = urun.Fiyat;
            model.Id = urun.Id;
            model.KargolamaSuresiId = urun.KargolamaSuresiId;
            model.KisaAciklama = urun.KisaAciklama;
            model.Maliyet = urun.Maliyet;
            model.OzelFiyat = urun.OzelFiyat;
            model.OzelFiyatBaslangicTarihi = urun.OzelFiyatBaslangicTarihi;
            model.OzelFiyatBitisTarihi = urun.OzelFiyatBitisTarihi;
            model.ShowOnHomePage = urun.ShowOnHomePage;
            model.Sira = urun.Sira;
            model.StokAdeti = urun.StokAdeti;
            model.StokKodu = urun.StokKodu;
            model.StoklardanDussun = urun.StoklardanDussun;
            model.UrunAdi = urun.UrunAdi;
            model.Visibility = urun.Visibility;
            model.KargoDurum = urun.KargoDurum;
            model.KargoFiyat = urun.KargoFiyat;
            model.Categories = urun.Kategori_Urun_Mapping.Select(f => f.KategoriId).ToArray();
            model.YukluResimler = _pictureService.GetPicturesByIds(urun.UrunResim.OrderBy(f => f.Sira).Select(f => f.ResimId).ToArray()).ToList().Select(f => f.DosyaYol);
            return model;
        }
    }
}