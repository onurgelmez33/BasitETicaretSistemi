using ETicaret.Data;
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
    public class SliderController : Controller
    {
        private ISlideshowService _slideshowService;
        private IPictureService _pictureService;
        public SliderController(ISlideshowService slideshowService, IPictureService pictureService)
        {
            _slideshowService = slideshowService;
            _pictureService = pictureService;
        }
        // GET: Admin/Slider
        public ActionResult Index()
        {
            var model = _slideshowService.GetAllSlides().ToList().Select(f => PrepareSlideIndexModel(f));
            return View(model);
        }

        public ActionResult Olustur()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Olustur(SlideCreateEditModel model)
        {
            if (ModelState.IsValid)
            {
                _slideshowService.Create(new Slideshow
                {
                    Aciklama = model.Aciklama,
                    Baslik = model.Baslik,
                    ButonAdi = model.ButonAdi,
                    ButonLink = model.ButonLink,
                    Id = model.Id
                }, model.Resim, Server.MapPath("~/uploads"));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Duzenle(int id)
        {
            var model = PrepareSlideCreateEditModel(_slideshowService.GetSlideById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(SlideCreateEditModel model)
        {
            if (ModelState.IsValid)
            {
                _slideshowService.Update(new Slideshow
                {
                    Aciklama = model.Aciklama,
                    Baslik = model.Baslik,
                    ButonAdi = model.ButonAdi,
                    ButonLink = model.ButonLink,
                    Id = model.Id
                }, model.Resim, Server.MapPath("~/uploads"));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public void Sil(int id)
        {
            _slideshowService.Delete(id);
        }

        [NonAction]
        private SlideIndexModel PrepareSlideIndexModel(Slideshow slideshow)
        {
            var model = new SlideIndexModel();
            model.Baslik = slideshow.Baslik;
            model.Id = slideshow.Id;
            model.ResimYol = _pictureService.GetPictureById(slideshow.ResimId).DosyaYol;
            return model;
        }

        [NonAction]
        private SlideCreateEditModel PrepareSlideCreateEditModel(Slideshow slideshow)
        {
            var model = new SlideCreateEditModel();
            model.Aciklama = slideshow.Aciklama;
            model.Baslik = slideshow.Baslik;
            model.ButonAdi = slideshow.ButonAdi;
            model.ButonLink = slideshow.ButonLink;
            model.Id = slideshow.Id;
            model.ResimId = _pictureService.GetPictureById(slideshow.ResimId).DosyaYol;
            return model;
        }
    }
}