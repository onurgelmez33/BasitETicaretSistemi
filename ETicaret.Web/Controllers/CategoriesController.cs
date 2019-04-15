using ETicaret.Data;
using ETicaret.Services.Catalog;
using ETicaret.Services.System;
using ETicaret.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Web.Controllers
{
    public class CategoriesController : Controller
    {
        #region Constructer
        private IUrunService _urunService;
        private IPictureService _pictureService;
        private IKategoriService _kategoriService;
        public CategoriesController(IUrunService urunService,
            IPictureService pictureService,
            IKategoriService kategoriService)
        {
            _urunService = urunService;
            _pictureService = pictureService;
            _kategoriService = kategoriService;
        }
        #endregion
        // GET: Categories
        public ActionResult Details(string id)
        {
            var page = Request.Params["page"];
            var order = Request.Params["order"];
            var baslangicFiyat = Request.Params["baslangicFiyat"];
            var bitisFiyat = Request.Params["bitisFiyat"];
            decimal bFiyat = 0;
            decimal bitFiyat = 0;
            int pageNumber = string.IsNullOrEmpty(page) ? 0 : int.Parse(page);
            var cat = _kategoriService.GetCategoryBySlug(id);
            ViewBag.MaxPrice = cat.Kategori_Urun_Mapping.Select(f => f.Urun).OrderByDescending(f => f.Fiyat).FirstOrDefault().Fiyat;
            var model = new CategoryModel
            {
                Name = cat.Adi,
                Short = cat.KisaAciklama,
                Image = _pictureService.GetPictureById(cat.ResimId ?? 0) != null ? _pictureService.GetPictureById(cat.ResimId ?? 0).DosyaYol : "/Content/noimage.jpg",
                Products = _kategoriService.GetProductsByCategoryId(cat.Id).ToList().Select(f => PrepareProductModel(f, false))
            };
            if (order == "1")
            {
                model.Products = model.Products.OrderByDescending(f => f.Viewed);
            }
            else if (order == "2")
            {
                model.Products = model.Products.OrderBy(f => f.Price);
            }
            else if (order == "3")
            {
                model.Products = model.Products.OrderByDescending(f => f.Price);
            }
            if (!string.IsNullOrEmpty(baslangicFiyat) && decimal.TryParse(baslangicFiyat, out bFiyat))
            {
                model.Products = model.Products.Where(f => f.Price >= bFiyat);
            }
            if (!string.IsNullOrEmpty(bitisFiyat) && decimal.TryParse(bitisFiyat, out bitFiyat))
            {
                model.Products = model.Products.Where(f => f.Price <= bitFiyat);
            }
            ViewBag.ProductCount = model.Products.Count();
            model.Products = model.Products.Skip((pageNumber - 1) * 8).Take(8);
            return View("~/Views/Categories/Details.cshtml", model);
        }
        [NonAction]
        public ProductModel PrepareProductModel(Urun urun, bool getRelated = false)
        {
            ProductModel model = new ProductModel();
            model.Slug = urun.Slug;
            model.Description = urun.KisaAciklama;
            model.Full = urun.Aciklama;
            model.Name = urun.UrunAdi;
            model.OldPrice = urun.EskiFiyat;
            model.Price = urun.Fiyat;
            model.ShipmentDay = urun.KargolamaSure.KargolamaSuresi;
            model.SpecialPrice = urun.OzelFiyat;
            model.SpecialPriceStartDate = urun.OzelFiyatBaslangicTarihi;
            model.SpecialPriceEndDate = urun.OzelFiyatBitisTarihi;
            model.Stock = urun.StoklardanDussun ? -1 : urun.StokAdeti;
            var picIds = urun.UrunResim.OrderBy(f => f.Sira).Select(f => f.ResimId);
            model.Pictures = _pictureService.GetPicturesByIds(picIds.ToArray()).Select(f => f.DosyaYol).ToList();
            if (getRelated)
            {
                model.RelatedProducts = _urunService.GetRelatedProductsById(urun.Id).Select(f => PrepareProductModel(f, false)).ToList();
            }
            return model;
        }
    }
}