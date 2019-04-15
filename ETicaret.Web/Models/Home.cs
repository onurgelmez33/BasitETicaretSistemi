using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Web.Models
{
    public class HomePageModel
    {
        public IEnumerable<ProductModel> FeaturedProducts { get; set; }
        public IEnumerable<HomePageCategoriesModel> FeaturedCategories { get; set; }
        public ProductModel SpecialProduct { get; set; }
        public List<HomePageBlogModel> Blog { get; set; }
        public List<SlideshowModel> Slider { get; set; }
        public string SpecialProductBanner { get; set; }
    }

    public class SlideshowModel
    {
        public string SlideImage { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string ButonAdi { get; set; }
        public string ButonLink { get; set; }
    }
    public class HomePageCategoriesModel
    {
        public string Baslik { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public int Sira { get; set; }
    }

    public class HomePageBlogModel
    {
        public string Baslik { get; set; }
        public string KisaAciklama { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public string Tarih { get; set; }
    }
}