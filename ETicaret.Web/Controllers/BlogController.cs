using ETicaret.Data;
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
    public class BlogController : Controller
    {
        #region Constructer
        private IPictureService _pictureService;
        private ISettingService _settingService;
        private IBlogService _blogService;
        public BlogController(
            IPictureService pictureService,
            ISettingService settingService,
            IBlogService blogService)
        {
            _pictureService = pictureService;
            _settingService = settingService;
            _blogService = blogService;
        }
        #endregion

        public ActionResult Index()
        {
            var model = _blogService.GetAllPosts().OrderByDescending(f => f.OlusturulmaTarihi).ToList().Select(f => PrepareBlogDetailModel(f));
            return View("~/Views/Blog/Index.cshtml", model);
        }
        // GET: Blog
        public ActionResult Details(string id)
        {
            var post = _blogService.GetPostBySlug(id);
            var model = PrepareBlogDetailModel(post);
            return View("~/Views/Blog/Details.cshtml", model);
        }
        private BlogDetailsModel PrepareBlogDetailModel(Blog blog)
        {
            var model = new BlogDetailsModel();
            model.CreatedOn = blog.OlusturulmaTarihi.ToString("dd MMM, yyyy");
            model.Full = blog.Aciklama;
            model.Image = _pictureService.GetPictureById(blog.ResimId ?? 0).DosyaYol;
            model.Name = blog.Adi;
            model.Short = blog.KisaAciklama;
            model.Tags = blog.Tags;
            model.Slug = blog.Slug;
            return model;
        }
    }
}