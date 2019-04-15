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
    public class ProductsController : Controller
    {
        private IUrunService _urunService;
        private IPictureService _pictureService;
        public ProductsController(IUrunService urunService,
            IPictureService pictureService)
        {
            _urunService = urunService;
            _pictureService = pictureService;
        }
        // GET: Products
        public ActionResult Details(string id)
        {
            var urun = _urunService.GetProductBySlug(id);
            if (urun != null)
            {
                ProductModel model = PrepareProductModel(urun, true);
                return View("~/Views/Products/Details.cshtml", model);
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }

        [NonAction]
        public ProductModel PrepareProductModel(Urun urun, bool getRelated = false)
        {
            ProductModel model = new ProductModel();
            model.Id = urun.Id;
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
            model.ShippingPrice = urun.KargoFiyat;
            model.FreeShipping = urun.KargoDurum;
            var picIds = urun.UrunResim.OrderBy(f => f.Sira).Select(f => f.ResimId);
            model.Pictures = _pictureService.GetPicturesByIds(picIds.ToArray()).Select(f => f.DosyaYol).ToList();
            if (getRelated)
            {
                model.RelatedProducts = _urunService.GetRelatedProductsById(urun.Id).ToList().Select(f => PrepareProductModel(f, false)).ToList();
            }
            return model;
        }
    }
}