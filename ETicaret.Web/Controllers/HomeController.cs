using ETicaret.Data;
using ETicaret.Services.Catalog;
using ETicaret.Services.CMS;
using ETicaret.Services.System;
using ETicaret.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Constructer
        private IUrunService _urunService;
        private IPictureService _pictureService;
        private ISettingService _settingService;
        private ISlideshowService _slideshowService;
        private IKategoriService _kategoriService;
        private IBlogService _blogService;
        private IRouteService _routeService;
        public HomeController(IUrunService urunService,
            IPictureService pictureService,
            ISettingService settingService,
            ISlideshowService slideshowService,
            IKategoriService kategoriService,
            IBlogService blogService,
            IRouteService routeService)
        {
            _urunService = urunService;
            _pictureService = pictureService;
            _settingService = settingService;
            _slideshowService = slideshowService;
            _kategoriService = kategoriService;
            _blogService = blogService;
            _routeService = routeService;
        }
        #endregion

        #region Actions
        // GET: Home
        //[AllowAnonymous] Giris Yapmadan girebilsin
        public ActionResult Index()
        {
            var model = new HomePageModel()
            {
                FeaturedProducts = _urunService.GetFeaturedProducts().ToList().Select(f => PrepareProductModel(f, false)),
                SpecialProduct = _urunService.GetSpecialProducts().Any() ? PrepareProductModel(_urunService.GetSpecialProducts().FirstOrDefault()) : null,
                SpecialProductBanner = _settingService.GetSetting<string>("HomeSpecialAreaBanner"),
                Slider = PrepareSlideshowModel(_slideshowService.GetHomePageSlides()),
                FeaturedCategories = PrepareHomePageCategoriesModel(_kategoriService.GetHomePageCategories()),
                Blog = PrepareHomePageBlogModel(_blogService.GetAllPosts().OrderByDescending(f => f.OlusturulmaTarihi))
            };
            return View(model);
        }

        public ActionResult CheckRoute(string id)
        {
            var controller = _routeService.UrlKontrolEt(id);
            if (controller == "Products")
            {
                var defaultController = new ProductsController(_urunService, _pictureService);
                defaultController.ControllerContext = new ControllerContext(
                    this.ControllerContext.RequestContext,
                    defaultController
                );
                return defaultController.Details(id);
            }
            else if (controller == "Categories")
            {
                var defaultController = new CategoriesController(_urunService, _pictureService, _kategoriService);
                defaultController.ControllerContext = new ControllerContext(
                    this.ControllerContext.RequestContext,
                    defaultController
                );
                return defaultController.Details(id);
            }
            else if (controller == "Blog")
            {
                var defaultController = new BlogController(_pictureService, _settingService, _blogService);
                defaultController.ControllerContext = new ControllerContext(
                    this.ControllerContext.RequestContext,
                    defaultController
                );
                return defaultController.Details(id);
            }
            else if (controller == "Error")
            {
                return View("Error");
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }
        #endregion

        #region Helper Funcs
        [NonAction]
        public ProductModel PrepareProductModel(Urun urun, bool getRelated = false)
        {
            ProductModel model = new ProductModel();
            model.Id = urun.Id;
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
            model.FreeShipping = urun.KargoDurum;
            model.ShippingPrice = urun.KargoFiyat;
            var picIds = urun.UrunResim.OrderBy(f => f.Sira).Select(f => f.ResimId);
            model.Pictures = _pictureService.GetPicturesByIds(picIds.ToArray()).Select(f => f.DosyaYol).ToList();
            if (getRelated)
            {
                model.RelatedProducts = _urunService.GetRelatedProductsById(urun.Id).Select(f => PrepareProductModel(f, false)).ToList();
            }
            return model;
        }
        [NonAction]
        public List<SlideshowModel> PrepareSlideshowModel(IQueryable<Slideshow> slides)
        {
            var data = new List<SlideshowModel>();
            foreach (var item in slides)
            {
                SlideshowModel slide = new SlideshowModel();
                slide.Aciklama = item.Aciklama;
                slide.Baslik = item.Baslik;
                slide.ButonAdi = item.ButonAdi;
                slide.ButonLink = item.ButonLink;
                var ids = new int[1] { item.ResimId };
                slide.SlideImage = _pictureService.GetPicturesByIds(ids).Any() ? _pictureService.GetPicturesByIds(ids).FirstOrDefault().DosyaYol : "/";
                data.Add(slide);
            }
            return data;
        }
        [NonAction]
        public List<HomePageCategoriesModel> PrepareHomePageCategoriesModel(IQueryable<Kategori> kategoriler)
        {
            var data = new List<HomePageCategoriesModel>();
            foreach (var item in kategoriler)
            {
                var kategori = new HomePageCategoriesModel();
                kategori.Baslik = item.Adi;
                kategori.Slug = item.Slug;
                if (item.ResimId == null)
                {
                    kategori.Image = "/Content/noimage.jpg";
                }
                else
                {
                    kategori.Image = _pictureService.GetPictureById(item.ResimId ?? 0) != null ? _pictureService.GetPictureById(item.ResimId ?? 0).DosyaYol : "/Content/noimage.jpg";
                }
                data.Add(kategori);
            }
            return data;
        }
        public List<HomePageBlogModel> PrepareHomePageBlogModel(IQueryable<Blog> posts)
        {
            var data = new List<HomePageBlogModel>();
            foreach (var item in posts)
            {
                var post = new HomePageBlogModel();
                post.Baslik = item.Adi;
                post.KisaAciklama = item.KisaAciklama;
                post.Slug = item.Slug;
                post.Tarih = item.OlusturulmaTarihi.ToString("MMM dd, yyyy");
                if (item.ResimId == null)
                {
                    post.Image = "/Content/noimage.jpg";
                }
                else
                {
                    post.Image = _pictureService.GetPictureById(item.ResimId ?? 0) != null ? _pictureService.GetPictureById(item.ResimId ?? 0).DosyaYol : "/Content/noimage.jpg";
                }
                data.Add(post);
            }
            return data;
        }
        #endregion
    }
}