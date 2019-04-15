using ETicaret.Data;
using ETicaret.Services.Catalog;
using ETicaret.Services.Kullanici;
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
    public class CategoriesController : Controller
    {
        private IKategoriService _kategoriService;
        private IPictureService _pictureService;
        private IKullaniciService _kullaniciService;
        private int UserId => _kullaniciService.GetUserByEmail(User.Identity.Name).Id;
        public CategoriesController(IKategoriService kategoriService, IPictureService pictureService, IKullaniciService kullaniciService)
        {
            _kategoriService = kategoriService;
            _pictureService = pictureService;
            _kullaniciService = kullaniciService;
        }
        // GET: Admin/Kategori
        public ActionResult Index()
        {
            var data = _kategoriService.GetAll().ToList().Select(f => PrepareKategoriIndexModel(f));
            return View(data);
        }


        public void Sil(int id)
        {
            _kategoriService.DeleteCategory(id);
        }

        public ActionResult Duzenle(int id)
        {
            var data = _kategoriService.GetCategoryById(id);
            ViewBag.UstKategoriId = new SelectList(_kategoriService.GetAll().Where(f => f.Id != id), "Id", "Adi", data.UstKategoriId);
            return View(PrepareKategoriEditCreateModel(data));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(KategoriEditCreateModel model)
        {
            if (ModelState.IsValid)
            {
                _kategoriService.UpdateCategory(new Kategori
                {
                    KullaniciId = UserId,
                    Aciklama = model.Aciklama,
                    Adi = model.Adi,
                    KisaAciklama = model.KisaAciklama,
                    ShowOnHomePage = model.ShowOnHomePage,
                    Sira = model.Sira,
                    UstKategoriId = model.UstKategoriId,
                    Id = model.Id,
                    OlusturulmaTarihi = DateTime.Now,
                    Slug = Server.MapPath("~/uploads")
                }, model.Resim);
                return RedirectToAction("Index");
            }
            ViewBag.UstKategoriId = new SelectList(_kategoriService.GetAll(), "Id", "Adi", model.UstKategoriId);
            return View(model);
        }

        public ActionResult Olustur()
        {
            ViewBag.UstKategoriId = new SelectList(_kategoriService.GetAll(), "Id", "Adi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Olustur(KategoriEditCreateModel model)
        {
            if (ModelState.IsValid)
            {
                _kategoriService.CreateCategory(new Kategori
                {
                    KullaniciId = UserId,
                    Aciklama = model.Aciklama,
                    Adi = model.Adi,
                    KisaAciklama = model.KisaAciklama,
                    ShowOnHomePage = model.ShowOnHomePage,
                    Sira = model.Sira,
                    UstKategoriId = model.UstKategoriId,
                    OlusturulmaTarihi = DateTime.Now,
                    Slug = Server.MapPath("~/uploads")
                }, model.Resim);
                return RedirectToAction("Index");
            }
            ViewBag.UstKategoriId = new SelectList(_kategoriService.GetAll(), "Id", "Adi", model.UstKategoriId);
            return View(model);
        }

        [NonAction]
        private KategoriIndexModel PrepareKategoriIndexModel(Kategori kategori)
        {
            var model = new KategoriIndexModel();
            model.Id = kategori.Id;
            model.Name = kategori.Adi;
            model.ResimYol = _pictureService.GetPictureById(kategori.ResimId ?? 0).DosyaYol;
            model.Sira = kategori.Sira;
            model.UrunSayisi = _kategoriService.GetProductsByCategoryId(kategori.Id).Count();
            return model;
        }

        [NonAction]
        private KategoriEditCreateModel PrepareKategoriEditCreateModel(Kategori kategori)
        {
            var model = new KategoriEditCreateModel();
            model.Aciklama = kategori.Aciklama;
            model.Adi = kategori.Adi;
            model.Id = kategori.Id;
            model.KisaAciklama = kategori.KisaAciklama;
            model.ResimUrl = _pictureService.GetPictureById(kategori.ResimId ?? 0).DosyaYol;
            model.ShowOnHomePage = kategori.ShowOnHomePage;
            model.Sira = kategori.Sira;
            model.UstKategoriId = kategori.UstKategoriId;
            return model;
        }
    }
}