using ETicaret.Data;
using ETicaret.Services.CMS;
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
    public class PostsController : Controller
    {
        private IBlogService _blogService;
        private IPictureService _pictureService;
        public PostsController(IBlogService blogService,
             IPictureService pictureService)
        {
            _blogService = blogService;
            _pictureService = pictureService;
        }
        // GET: Admin/Blog
        public ActionResult Index()
        {
            var data = _blogService.GetAllPosts().OrderByDescending(f => f.OlusturulmaTarihi).ToList().Select(f => PrepareBlogIndexModel(f));
            return View(data);
        }

        public void Sil(int id)
        {
            _blogService.DeletePost(id);
        }

        public ActionResult Duzenle(int id)
        {
            var data = _blogService.GetPostById(id);
            return View(PrepareBlogEditCreateModel(data));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(BlogEditCreateModel model)
        {
            if (ModelState.IsValid)
            {
                _blogService.UpdatePost(new Blog
                {
                    Aciklama = model.Full,
                    Adi = model.Name,
                    Id = model.Id,
                    KisaAciklama = model.Short,
                    OlusturulmaTarihi = DateTime.Now,
                    Tags = model.Tags,
                    Slug = Server.MapPath("~/uploads")
                }, model.Picture);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Olustur()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Olustur(BlogEditCreateModel model)
        {
            if (ModelState.IsValid)
            {
                _blogService.CreatePost(new Blog
                {
                    Aciklama = model.Full,
                    Adi = model.Name,
                    Id = model.Id,
                    KisaAciklama = model.Short,
                    OlusturulmaTarihi = DateTime.Now,
                    Tags = model.Tags,
                    Slug = Server.MapPath("~/uploads")
                }, model.Picture);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [NonAction]
        private BlogIndexModel PrepareBlogIndexModel(Blog blog)
        {
            var model = new BlogIndexModel();
            model.CreatedOn = blog.OlusturulmaTarihi;
            model.Id = blog.Id;
            model.Name = blog.Adi;
            model.PictureUrl = _pictureService.GetPictureById(blog.ResimId ?? 0).DosyaYol;
            return model;
        }

        [NonAction]
        private BlogEditCreateModel PrepareBlogEditCreateModel(Blog blog)
        {
            var model = new BlogEditCreateModel();
            model.CreatedOn = blog.OlusturulmaTarihi;
            model.Id = blog.Id;
            model.Name = blog.Adi;
            model.Full = blog.Aciklama;
            model.Short = blog.KisaAciklama;
            model.Tags = blog.Tags;
            model.PictureUrl = _pictureService.GetPictureById(blog.ResimId ?? 0).DosyaYol;
            return model;
        }
    }
}