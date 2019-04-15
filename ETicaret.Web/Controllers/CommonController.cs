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
    public class CommonController : Controller
    {
        #region Constructer
        private ISettingService _settingService;
        private IKategoriService _kategoriService;
        public CommonController(ISettingService settingService,
            IKategoriService kategoriService)
        {
            _settingService = settingService;
            _kategoriService = kategoriService;
        }
        #endregion

        #region Helper Funcs
        [NonAction]
        public List<HeaderCategoriesModel> PrepareHeaderCategoriesModel(IQueryable<Kategori> kategoriler)
        {
            var data = new List<HeaderCategoriesModel>();
            foreach (var item in kategoriler)
            {
                var kategori = new HeaderCategoriesModel();
                kategori.Baslik = item.Adi;
                kategori.Link = item.Slug;
                data.Add(kategori);
            }
            return data;
        }
        #endregion

        #region Actions
        // GET: Common
        public ActionResult Header()
        {
            var model = new HeaderModel();
            model.Logo = _settingService.GetSetting<string>("LogoUrl");
            model.LogoWidth = _settingService.GetSetting<int>("LogoWidth");
            model.LogoHeight = _settingService.GetSetting<int>("LogoHeight");
            model.Categories = PrepareHeaderCategoriesModel(_kategoriService.GetHeaderCategories());
            return View(model);
        }

        public ActionResult Footer()
        {
            return View();
        }
        #endregion
    }
}